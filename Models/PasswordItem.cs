namespace password_manager_backend.Models
{
    public class PasswordItem
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Password { get; set; }
        public string App { get; set; }
        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
