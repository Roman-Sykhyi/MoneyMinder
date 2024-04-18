using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceManagerBack.Models
{
    public class RegularPayment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public int Period { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public int WalletId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RegularPayment payment &&
                   Id == payment.Id &&
                   Name == payment.Name &&
                   Start.Date == payment.Start.Date &&
                   Period == payment.Period &&
                   Amount == payment.Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Start.Day, Period, Amount);
        }
    }
}
