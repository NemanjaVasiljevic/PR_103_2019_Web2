using AutoMapper;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;

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

        public List<UserDto> GetAllUsers()
        {
            return mapper.Map<List<UserDto>>(dbContext.User.ToList());
        }

        public UserDto RegisterUser(UserDto user)
        {
            User userDb = mapper.Map<User>(user);

            dbContext.Add(userDb);
            try
            {
               dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return mapper.Map<UserDto>(userDb);
        }
    }
}
