namespace password_manager_backend.Models
{
    public class PasswordViewModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string App { get; set; }
        public string UserName { get; set; }
        // KEIN Password oder EncryptedPassword – das bleibt geheim
    }
}
