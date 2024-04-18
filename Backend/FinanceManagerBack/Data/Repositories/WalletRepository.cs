using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagerBack.Data.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationContext _context;
        public WalletRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Wallet>> GetWalletsAsync(string email)
        {
            var user = await _context.Users.Include(u => u.Wallets).ThenInclude(w=>w.Transactions).SingleOrDefaultAsync(u => u.Email == email);
            return user.Wallets;
        }

        public async Task<Wallet> GetWalletAsync(int id, string email)
        {
            var user = await _context.Users.Include(u => u.Wallets).ThenInclude(w => w.Transactions).SingleOrDefaultAsync(u => u.Email == email);
            var wallets = user.Wallets;
            return wallets.FirstOrDefault(wallet => wallet.Id == id);

        }

        public async Task AddWalletAsync(Wallet wallet, string email)
        {
            var user = await _context.Users.Include(u => u.Wallets).SingleOrDefaultAsync(u => u.Email == email);
            user.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task<Wallet> DeleteWalletAsync(int id,string email)
        {
            var user = await _context.Users
                .Include(u => u.Wallets).ThenInclude(w=>w.Transactions)
                .Include(u=>u.Wallets).ThenInclude(w=>w.RegularPayments)
                .Include(u=>u.Wallets).ThenInclude(w=>w.CategoryLimits)
                .SingleOrDefaultAsync(u => u.Email == email);
            
            var wallets = user.Wallets;

            if(wallets==null)
                return null;

            if (!wallets.Any())
                return null;

            var wallet = wallets.FirstOrDefault(w => w.Id == id);
            
            if (wallet == null)
                return null;

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }
    }
}
