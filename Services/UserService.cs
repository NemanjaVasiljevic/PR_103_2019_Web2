﻿using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PR_103_2019.Services
{
    public class UserService : IUserService
    {
        private PR_103_2019Context dbContext;
        private IMapper mapper;
        private readonly IConfigurationSection secretKey;

        public UserService(PR_103_2019Context db, IMapper map,IConfiguration key)
        {
            dbContext = db;
            mapper = map;
            secretKey = key.GetSection("Secret Key");
        }

        private string ComputeHmac(string data, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] hmacBytes = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hmacBytes).Replace("-", "").ToLower();
            }
        }

        public List<UserDto> GetAllUsers()
        {
            return mapper.Map<List<UserDto>>(dbContext.User.ToList());
        }

        public UserDto GetUserById(long id)
        {
            UserDto user = mapper.Map<UserDto>(dbContext.User.Find(id));
            if(user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        public string Login(UserDto loginUser)
        {
            User user = dbContext.User.FirstOrDefault(u => u.Username == loginUser.Username);

            if(user == null)
            {
                throw new Exception("Incorrect username!");
            }
            
            if(!ComputeHmac(loginUser.Password, loginUser.Username).Equals(ComputeHmac(user.Password, user.Username)))
            {
                throw new Exception("Incorrect password!");
            }


            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            if (user.Role == Role.SELLER && user.VerificationStatus == VerificationState.ACCEPTED)
            {
                claims.Add(new Claim("VerificationStatus", user.VerificationStatus.ToString()));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey.Value));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: "http://localhost:44319",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


        public UserDto RegisterUser(UserDto user)
        {
            User userDb = mapper.Map<User>(user);
            userDb.Password = ComputeHmac(userDb.Password, userDb.Username);

            if(user.Role == Role.SELLER || user.Role == Role.ADMIN)
            {
                userDb.VerificationStatus = VerificationState.ACCEPTED;
            }
            else
            {
                userDb.VerificationStatus = VerificationState.PENDING;
            }

            dbContext.Add(userDb);
            try
            {
               dbContext.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }

            return mapper.Map<UserDto>(userDb);
        }

        public bool DeleteUser(long id)
        {
            User userDb = dbContext.User.Find(id);
            if(userDb != null)
            {
                dbContext.Remove(userDb);

                try
                {
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            else
            {
                return false;
            }


        }
    }
}
