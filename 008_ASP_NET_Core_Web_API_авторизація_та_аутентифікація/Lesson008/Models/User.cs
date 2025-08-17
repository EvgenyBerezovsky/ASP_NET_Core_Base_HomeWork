namespace Lesson008.Models
{
    public class User
    {
        public required string Name { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required Position Position { get; set; }
        public required Department Department { get; set; }
    }
}
