using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NatureNexus.Models
{
    public class ResetPasswordViewModel
    {

        [DisplayName("Eposta")]
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [EmailAddress(ErrorMessage = "Geçersiz Eposta")]
        public string EMail { get; set; }
        public string Code { get; set; }

        [NotMapped]
        [DisplayName("Parola")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "En fazla 128, en az 8 karakter")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; } = default!;
    }
}
