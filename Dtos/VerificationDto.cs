using PR_103_2019.Models;

namespace PR_103_2019.Dtos
{
    public class VerificationDto
    {
        public long UserId { get; set; }
        public VerificationState VerificationStatus { get; set; }
    }
}
