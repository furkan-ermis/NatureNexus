using System.ComponentModel.DataAnnotations;

namespace NatureNexus.Models
{
    public class UserLogInViewModel
    {
        [Required(ErrorMessage = "Lütfen Email Adresinizi Giriniz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
