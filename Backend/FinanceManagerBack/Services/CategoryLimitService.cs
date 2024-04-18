using FinanceManagerBack.Data.Category;
using FinanceManagerBack.Enums;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;

namespace FinanceManagerBack.Services
{
    public class CategoryLimitService : ICategoryLimitService
    {
        private readonly ICategoryLimitRepository _categoryLimitRepository;
        private readonly IStatisticsService _statisticsService;

        public CategoryLimitService(ICategoryLimitRepository categoryLimitRepository, IStatisticsService statisticsService)
        {
            _categoryLimitRepository = categoryLimitRepository;
            _statisticsService = statisticsService;
        }

        public CategoryLimit GetCategoryLimit(int walletId, int categoryId)
        {
            return _categoryLimitRepository.GetByWalletAndCategory(walletId, categoryId);
        }

        public void SendCategoryLimit(CategoryLimitDto categoryLimitDto)
        {
            CategoryLimit categoryLimit = _categoryLimitRepository.GetByWalletAndCategory(categoryLimitDto.WalletId, categoryLimitDto.CategoryId);

            if(categoryLimit != null)
            {
                if(categoryLimitDto.Limit == 0)
                {
                    _categoryLimitRepository.Delete(categoryLimit);
                }
                else
                {
                    _categoryLimitRepository.UpdateLimit(categoryLimit, categoryLimitDto.Limit);
                }
            }
            else
            {
                _categoryLimitRepository.AddNewLimit(categoryLimitDto);
            }
        }

        public LimitAlertEnum CheckLimit(int walletId, int categoryId)
        {
            var statistics = _statisticsService.GetStatisticsForPeriod("Month", walletId, categoryId);
            int spent = statistics[0];
            var categoryLimit = GetCategoryLimit(walletId, categoryId);

            if (categoryLimit == null)
                return LimitAlertEnum.None;

            int limit = -categoryLimit.Limit;

            double percent = (double)spent / limit;

            if (percent >= 1)
                return LimitAlertEnum.Exceeded;
            else if (percent >= 0.7)
                return LimitAlertEnum.Approaching;
            else
                return LimitAlertEnum.None;
        }
    }
}