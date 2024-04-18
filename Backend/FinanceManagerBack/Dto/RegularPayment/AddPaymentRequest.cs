using System.ComponentModel.DataAnnotations;

namespace FinanceManagerBack.Dto.RegularPayment
{
    public class AddPaymentRequest
    {
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter date")]
        public int Date { get; set; }

        [Required(ErrorMessage = "Enter period")]
        public int Period { get; set; }

        [Required(ErrorMessage = "Enter amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Enter Wallet Id")]
        public int WalletId { get; set; }
    }
}
