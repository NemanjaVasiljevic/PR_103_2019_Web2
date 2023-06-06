using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.IdentityModel.Tokens;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Exceptions;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PR_103_2019.Services
{
    public class UserService : IUserService
    {
        private PR_103_2019Context dbContext;
        private IMapper mapper;
        private readonly IConfigurationSection _secretKey;

        public UserService(IConfiguration config, PR_103_2019Context db, IMapper map)
        {
            _secretKey = config.GetSection("Secret Key");
            dbContext = db;
            mapper = map;
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

        public string Login(LoginDto loginUser)
        {
            User user = dbContext.User.FirstOrDefault(u => u.Email == loginUser.Email);

            if(user == null)
            {
                throw new InvalidCredentialsException("Incorrect email!");
            }
            
            if(!user.Password.Equals(ComputeHmac(loginUser.Password, loginUser.Email)))
            {
                throw new InvalidCredentialsException("Incorrect password!");
            }


            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            if (user.Role == Role.SELLER && user.VerificationStatus == VerificationState.ACCEPTED)
            {
                claims.Add(new Claim("VerificationStatus", user.VerificationStatus.ToString()));
            }

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeySecretKey"));

            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: "http://localhost:44319",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public UserDto GetUserByEmail(string email)
        {
            User user = dbContext.User.FirstOrDefault(u => u.Email == email);
            
            return mapper.Map<UserDto>(user);
        }

        public UserDto RegisterUser(UserDto user)
        {
            User userDb = mapper.Map<User>(user);
            userDb.Password = ComputeHmac(userDb.Password, userDb.Email);

            User userEmail = dbContext.User.FirstOrDefault(u => u.Email == user.Email);
            if(userEmail != null)
            {
                throw new InvalidCredentialsException("User with specified username and/or email already exists!");
            }


            if(user.Role == Role.ADMIN || user.Role == Role.USER)
            {
                userDb.VerificationStatus = VerificationState.ACCEPTED;
            }
            else if(user.Role == Role.SELLER)
            {
                userDb.VerificationStatus = VerificationState.PENDING;
            }

            try
            {
                MailAddress userMail = new MailAddress(userDb.Email);
            }
            catch (FormatException)
            {

                throw new InvalidCredentialsException("Invalid email");
            }

            dbContext.Add(userDb);

            try
            {
               dbContext.SaveChanges();
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidFieldsException("One of more fields are missing!");
            }
            catch (Exception)
            {
                throw;
            }

            if (user.Role == Role.SELLER)
            {
                EmailSender mail = new EmailSender();
                mail.SendVerificationEmail(userDb.Email, "Vas nalog je kreiran admin ce potvrditi vasu verifikaciju." +
                                                         "Povratak na aplikaciju: http://localhost:3000/home");
                                        
            }
            return mapper.Map<UserDto>(userDb);
        }

        public UserDto UpdateUser(long id, UserDto userDto)
        {
            User user = dbContext.User.Find(id);

            if (user == null)
            {
                return null;
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.BirthDay = userDto.BirthDay;
            user.Address = userDto.Address;

            if (!ComputeHmac(userDto.Password, userDto.Email).Equals(ComputeHmac(user.Password, user.Email)))
            {
                user.Password = ComputeHmac(userDto.Password, user.Email);
            }

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }

            return mapper.Map<UserDto>(user);
        }

        public UserDto VerifyUser(VerificationDto verifyDto)
        {
            User user = dbContext.User.Find(verifyDto.UserId);

            if (user == null)
            {
                throw new ResourceNotFoundException("User with specified id doesn't exist!");
            }

            user.VerificationStatus = verifyDto.VerificationStatus;

            dbContext.SaveChanges();

            return mapper.Map<UserDto>(user);
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
