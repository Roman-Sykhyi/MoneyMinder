using System;


namespace FinanceManagerBack.Dto.Wallet
{
    public class WalletDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalSum { get; set; }
        public override bool Equals(object obj)
        {
            WalletDto wallet = obj as WalletDto;
            
            if (wallet == null)
                return false;

            return Id == wallet.Id && Name == wallet.Name && TotalSum == wallet.TotalSum;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id,Name,TotalSum);
        }
    }
}
