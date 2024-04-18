using FinanceManagerBack.Data.Category;
using FinanceManagerBack.Enums;
using FinanceManagerBack.Models;

namespace FinanceManagerBack.Interfaces
{
    public interface ICategoryLimitService
    {
        public CategoryLimit GetCategoryLimit(int walletId, int categoryId);
        public void SendCategoryLimit(CategoryLimitDto categoryLimitDto);
        public LimitAlertEnum CheckLimit(int walletId, int categoryId);
    }
}
