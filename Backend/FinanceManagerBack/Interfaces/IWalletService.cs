using FinanceManagerBack.Dto.Wallet;
using FinanceManagerBack.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagerBack.Interfaces
{
    public interface IWalletService
    {
        public Task<IEnumerable<WalletDto>> GetWalletsAsync(string email);

        public Task<WalletDto> GetWalletAsync(int id,string email);

        public Task AddWalletAsync(WalletDto wallet,string email);

        public Task<WalletDto> DeleteWalletAsync(int id,string email);

        public void Update(Wallet wallet);

        public Transaction CreateTransactionRegular(string name, decimal amount, DateTime date);
    }
}