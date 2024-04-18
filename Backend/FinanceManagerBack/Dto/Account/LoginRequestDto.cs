using System.ComponentModel.DataAnnotations;

namespace FinanceManagerBack.ViewModels.Account
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
    }
}