using System.ComponentModel.DataAnnotations;

namespace FinanceManagerBack.ViewModels.Account
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Enter Name")]
        [MinLength(5, ErrorMessage = "MinLength must be > 5")]
        [MaxLength(20, ErrorMessage = "MaxLength must be < 20")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [MinLength(6, ErrorMessage = "MinLength must be > 6")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password not equal!!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}