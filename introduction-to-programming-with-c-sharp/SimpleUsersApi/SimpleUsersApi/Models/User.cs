namespace SimpleUsersApi.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
