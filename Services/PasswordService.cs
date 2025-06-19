using System.Collections.Generic;
using System.Linq;
using System.Text;
using password_manager_backend.Models;

namespace password_manager_backend.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly List<PasswordItem> _passwords = new List<PasswordItem>();
        private int _nextId = 1;

        public IEnumerable<PasswordItem> GetAll() => _passwords;

        public PasswordItem GetById(int id) => _passwords.FirstOrDefault(p => p.Id == id);

        public PasswordItem GetDecryptedById(int id)
        {
            var item = GetById(id);
            if (item == null) return null;

            var decrypted = Encoding.UTF8.GetString(System.Convert.FromBase64String(item.EncryptedPassword));
            return new PasswordItem
            {
                Id = item.Id,
                Category = item.Category,
                App = item.App,
                UserName = item.UserName,
                Password = decrypted,
                EncryptedPassword = item.EncryptedPassword
            };
        }

        public PasswordItem Add(PasswordInputModel input)
        {
              var encrypted = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(input.Password)
            );

            var newItem = new PasswordItem
            {
                Id = _nextId++,
                Category = input.Category,
                App = input.App,
                UserName = input.UserName,
                EncryptedPassword = encrypted
            };

            _passwords.Add(newItem);
            return newItem;
        }

        public PasswordItem Update(int id, PasswordUpdateModel updated)
        {
            var existing = GetById(id);
            if (existing == null) return null;

            existing.Category = updated.Category;
            existing.App = updated.App;
            existing.UserName = updated.UserName;

            if (!string.IsNullOrEmpty(updated.Password))
            {
                existing.EncryptedPassword = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(updated.Password)
                );
            }

            return existing;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            _passwords.Remove(existing);
            return true;
        }
    }
}
