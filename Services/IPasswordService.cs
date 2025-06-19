using System.Collections.Generic;
using password_manager_backend.Models;

namespace password_manager_backend.Services
{
    public interface IPasswordService
    {
        IEnumerable<PasswordItem> GetAll();
        PasswordItem GetById(int id);
        PasswordItem GetDecryptedById(int id);
        PasswordItem Add(PasswordInputModel input);
        PasswordItem Update(int id, PasswordUpdateModel item);
        bool Delete(int id);
    }
}
