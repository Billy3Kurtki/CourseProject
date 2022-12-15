using System.ComponentModel.DataAnnotations;

namespace Microservice.Models.Authorization
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        public bool KeepLoggedIn { get; set; }
    }
}
