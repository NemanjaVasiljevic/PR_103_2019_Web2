using PR_103_2019.Dtos;

namespace PR_103_2019.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto RegisterUser(UserDto user);
        UserDto GetUserById(long id);
        string Login(LoginDto loginUser);
        UserDto UpdateUser(long id, UserDto userDto);
        UserDto VerifyUser(VerificationDto verifyDto);
        bool DeleteUser(long id);
    }
}
