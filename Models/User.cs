namespace PR_103_2019.Models
{
    public class User
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
        public bool Verified { get; set; }
        public List<Article> Articles { get; set; }
        public List<Order> Orders { get; set; }
    }
}
