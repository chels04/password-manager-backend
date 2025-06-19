namespace password_manager_backend.Models
{
    public class PasswordInputModel
    {
        public string Category { get; set; }
        public string App { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }  // Klartextpasswort
    }
}
