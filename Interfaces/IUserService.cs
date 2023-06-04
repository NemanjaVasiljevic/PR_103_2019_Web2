using PR_103_2019.Dtos;

namespace PR_103_2019.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto RegisterUser(UserDto user);
        UserDto GetUserById(long id);
        string Login(UserDto loginUser);
        bool DeleteUser(long id);
    }
}
