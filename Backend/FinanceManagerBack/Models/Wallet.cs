using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManagerBack.Models
{
    public class Wallet
    {
        public Wallet()
        {
            Transactions = new List<Transaction>();
            RegularPayments = new List<RegularPayment>();
            CategoryLimits = new List<CategoryLimit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<RegularPayment> RegularPayments { get; set; }
        public ICollection<CategoryLimit> CategoryLimits { get; set; }

         public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            if (obj.GetType() != typeof(Wallet))
                return false;

            Wallet wallet=obj as Wallet;

            return wallet.Id == Id 
                && wallet.Name == Name 
                && Transactions.SequenceEqual(wallet.Transactions) 
                && RegularPayments.SequenceEqual(wallet.RegularPayments);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}