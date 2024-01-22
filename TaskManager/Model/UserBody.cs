namespace TaskManager.Model
{
    public class UserBody
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class Login
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
