using FinanceManagerBack.Data.Category;
using FinanceManagerBack.Models;

namespace FinanceManagerBack.Interfaces
{
    public interface ICategoryLimitRepository
    {
        public CategoryLimit GetByWalletAndCategory(int walletId, int categoryId);
        public void Delete(CategoryLimit categoryLimit);
        public void UpdateLimit(CategoryLimit categoryLimit, int limit);
        public void AddNewLimit(CategoryLimitDto categoryLimitDto);
    }
}
