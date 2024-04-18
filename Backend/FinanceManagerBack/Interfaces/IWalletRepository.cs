using FinanceManagerBack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagerBack.Interfaces
{
    public interface IWalletRepository
    {
        public Task<IEnumerable<Wallet>> GetWalletsAsync(string email);

        public Task<Wallet> GetWalletAsync(int id,string email);

        public Task AddWalletAsync(Wallet wallet, string email);

        public Task<Wallet> DeleteWalletAsync(int id,string email);
    }
}
