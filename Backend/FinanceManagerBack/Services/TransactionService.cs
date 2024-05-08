using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagerBack.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationContext _context;

        public TransactionService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int walletId, int categoryId)
        {
            var wallet = await _context.Wallets.Include(w => w.Transactions).FirstOrDefaultAsync(w => w.Id == walletId);

            if(categoryId != -1)
            {
                return wallet.Transactions.Where(t => t.Category != null && t.Category.Id == categoryId);
            }

            return wallet.Transactions;
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            if (transaction.Category != null)
            {
                var newTransactionCategoryId = transaction.Category.Id;
                transaction.Category = _context.Categories.FirstOrDefault(o => o.Id == newTransactionCategoryId);
            }
            
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                return null;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public void FillDbWithRandomTransactions(int numberOfTransactions)
        {
            for (int i = 0; i < numberOfTransactions; i++)
            {
                Random random = new Random((int)DateTime.Now.AddDays(-i).Ticks);

                var categoryId = random.Next(3, 14);
                var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

                Transaction transaction = new Transaction
                {
                    Category = category,
                    Amount = (decimal)random.Next(-2000, 0),
                    WalletId = 1008,
                    CreationTime = DateTime.UtcNow.AddDays(-random.Next(1, 365)),
                    IsRegular = false,
                    Name = $"Random Transaction for regression{i + 1}"
                };

                _context.Transactions.Add(transaction);
            }

            _context.SaveChanges();
        }
    }
}