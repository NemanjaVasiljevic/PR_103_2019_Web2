using PR_103_2019.Models;

namespace PR_103_2019.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string? BirthDay { get; set; }
        public Role? Role { get; set; }
        public VerificationState? VerificationStatus { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
