using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceManagerBack.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        [Required]
        public int WalletId { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsRegular { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (Category == null && (obj as Transaction).Category == null)
            {
                return obj is Transaction temp &&
                  Id == temp.Id &&
                  Amount == temp.Amount &&
                  CreationTime.Date == temp.CreationTime.Date &&
                  IsRegular == temp.IsRegular &&
                  Name == temp.Name;
            }
                
            return obj is Transaction transaction &&
                   Id == transaction.Id &&
                   Category.Equals(transaction.Category) &&
                   Amount == transaction.Amount &&
                   CreationTime == transaction.CreationTime &&
                   IsRegular == transaction.IsRegular &&
                   Name == transaction.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Category, Amount, CreationTime, IsRegular, Name);
        }
    }
}