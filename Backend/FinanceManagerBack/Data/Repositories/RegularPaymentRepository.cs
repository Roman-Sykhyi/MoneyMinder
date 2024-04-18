using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagerBack.Data.Repositories
{
    public class RegularPaymentRepository : IRegularPaymentRepository
    {
        private readonly ApplicationContext db;

        public RegularPaymentRepository(ApplicationContext dbContext)
        {
            db = dbContext;
        }

        public async Task<bool> Add(RegularPayment entity, int walletId)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var wallets = db.Wallets.Include(e => e.RegularPayments).Include(e => e.Transactions);

            var wallet = await wallets.FirstOrDefaultAsync(x => x.Id == walletId);

            if (wallet != null)
            {
                var payments = wallet.RegularPayments;

                if (payments == null)
                    payments = new List<RegularPayment>();

                payments.Add(entity);

                var transactions = wallet.Transactions;

                if (transactions == null)
                    transactions = new List<Transaction>();

                var service = new WalletService();

                service.Update(wallet);

                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var payment = await db.RegularPayments.FirstOrDefaultAsync(x => x.Id == id);

            if (payment != null)
            {
                db.RegularPayments.Remove(payment);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<RegularPayment>> GetAll()
        {
            var payments = await db.RegularPayments.ToListAsync();

            if (payments != null)
            {
                return payments;
            }

            return null;
        }
        public async Task<RegularPayment> GetById(int id)
        {
            var payment = await db.RegularPayments.FirstOrDefaultAsync(x => x.Id == id);

            if (payment != null)
            {
                return payment;
            }
            return null;
        }

        public async Task<RegularPayment> GetByName(string name)
        {
            var payment = await db.RegularPayments.FirstOrDefaultAsync(x => x.Name == name);

            if (payment != null)
            {
                return payment;
            }
            return null;
        }

        public async Task<IEnumerable<RegularPayment>> GetByWalletId(int id)
        {
            var wallets = db.Wallets.Include(e => e.RegularPayments);

            var wallet = await wallets.FirstOrDefaultAsync(x => x.Id == id);

            if (wallet != null)
            {
                var payments = wallet.RegularPayments;

                if (payments == null)
                    payments = new List<RegularPayment>();

                return payments.ToList();
            }
            return null;
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
