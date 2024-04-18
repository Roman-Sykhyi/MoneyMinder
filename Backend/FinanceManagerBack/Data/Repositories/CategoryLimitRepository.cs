using FinanceManagerBack.Data.Category;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using System.Linq;

namespace FinanceManagerBack.Data.Repositories
{
    public class CategoryLimitRepository : ICategoryLimitRepository
    {
        private readonly ApplicationContext _context;

        public CategoryLimitRepository()
        {

        }

        public CategoryLimitRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual CategoryLimit GetByWalletAndCategory(int walletId, int categoryId)
        {
            return _context.CategoryLimits.Where(c => c.WalletId == walletId && c.CategoryId == categoryId).FirstOrDefault();
        }

        public virtual void Delete(CategoryLimit categoryLimit)
        {
            _context.Remove(categoryLimit);
            _context.SaveChanges();
        }

        public virtual void UpdateLimit(CategoryLimit categoryLimit, int limit)
        {
            categoryLimit.Limit = limit;
            _context.SaveChanges();
        }

        public virtual void AddNewLimit(CategoryLimitDto categoryLimitDto)
        {
            CategoryLimit categoryLimit = new CategoryLimit { WalletId = categoryLimitDto.WalletId, CategoryId = categoryLimitDto.CategoryId, Limit = categoryLimitDto.Limit };
            _context.CategoryLimits.Add(categoryLimit);
            _context.SaveChanges();
        }
    }
}