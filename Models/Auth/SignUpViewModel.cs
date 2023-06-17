
using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Auth
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "FullName is required.")]
        [MinLength(5, ErrorMessage = "The minimum lenght is 5.")]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("Password", ErrorMessage =
            "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

