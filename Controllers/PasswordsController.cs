using Microsoft.AspNetCore.Mvc;
using password_manager_backend.Models;
using password_manager_backend.Services;

namespace password_manager_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _service;

        public PasswordController(IPasswordService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var passwords = _service.GetAll();
            var dtoList = passwords.Select(p => new PasswordViewModel
            {
                Id = p.Id,
                Category = p.Category,
                App = p.App,
                UserName = p.UserName,
            }).ToList();
            
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();

            var dto = new PasswordViewModel
            {
                Id = item.Id,
                Category = item.Category,
                App = item.App,
                UserName = item.UserName,
            };

            return Ok(dto);
        }

        [HttpGet("decrypted/{id}")]
        public IActionResult GetDecryptedById(int id)
        {
            var item = _service.GetDecryptedById(id);
            if (item == null) return NotFound();
            
            return Ok(new
            {
                Id = item.Id,
                Category = item.Category,
                App = item.App,
                UserName = item.UserName,
                Password = item.Password
            });
        }

        [HttpPost]
        public IActionResult Add(PasswordInputModel input)
        {
            var created = _service.Add(input);

             var dto = new PasswordViewModel
            {
                Id = created.Id,
                Category = created.Category,
                App = created.App,
                UserName = created.UserName
            };

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PasswordUpdateModel item)
        {
            var updated = _service.Update(id, item);
            if (updated == null) return NotFound();

            var dto = new PasswordViewModel
            {
                Id = updated.Id,
                Category = updated.Category,
                App = updated.App,
                UserName = updated.UserName
            };

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
