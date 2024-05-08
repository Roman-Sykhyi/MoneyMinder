using FinanceManagerBack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagerBack.Interfaces
{
    public interface ITransactionService
    {
        public Task<IEnumerable<Transaction>> GetTransactionsAsync(int walletId, int categoryId);

        public Task<Transaction> GetTransactionAsync(int id);

        public Task AddTransactionAsync(Transaction transaction);

        public Task<Transaction> DeleteTransactionAsync(int id);

        public void FillDbWithRandomTransactions(int numberOfTransactions);
    }
}