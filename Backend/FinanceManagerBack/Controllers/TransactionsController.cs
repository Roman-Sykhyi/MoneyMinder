using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagerBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;
        private readonly ITransactionService _transactionService;

        public TransactionsController(ApplicationContext dbContext, ITransactionService transactionService)
        {
            _dbContext = dbContext;
            _transactionService = transactionService;
        }

        // GET: api/Transactions
        [HttpGet("all/{walletId}/{categoryId}")]

        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(int walletId, int categoryId)
        {
            var transactions = await _transactionService.GetTransactionsAsync(walletId, categoryId);

            if (!transactions.Any())
            {
                return NoContent();
            }

            return Ok(transactions);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NoContent();
            }

            return Ok(transaction);
        }

        // POST: api/Transactions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            await _transactionService.AddTransactionAsync(transaction);

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaction>> DeleteTransaction(int id)
        {
            var transaction = await _transactionService.DeleteTransactionAsync(id);
            if (transaction == null)
            {
                return NoContent();
            }

            return transaction;
        }

        private bool TransactionExists(int id)
        {
            return _dbContext.Transactions.Any(e => e.Id == id);
        }
    }
}