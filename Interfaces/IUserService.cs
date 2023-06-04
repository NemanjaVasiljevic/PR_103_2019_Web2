using PR_103_2019.Dtos;

namespace PR_103_2019.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto RegisterUser(UserDto user);

        bool DeleteUser(long id);
    }
}
