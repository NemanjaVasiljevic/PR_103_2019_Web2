using AutoMapper;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;
using System.Security.Cryptography;
using System.Text;

namespace PR_103_2019.Services
{
    public class UserService : IUserService
    {
        private PR_103_2019Context dbContext;
        private IMapper mapper;

        public UserService(PR_103_2019Context db, IMapper map)
        {
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

        public UserDto RegisterUser(UserDto user)
        {
            User userDb = mapper.Map<User>(user);
            userDb.Password = ComputeHmac(userDb.Password, userDb.Username);
            userDb.Verified = false;
            

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
