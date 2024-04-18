using System.ComponentModel.DataAnnotations;

namespace FinanceManagerBack.Models
{
    public class CategoryLimit
    {
        public int Id { get; set; }
        [Required]
        public int WalletId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int Limit { get; set; }
    }
}