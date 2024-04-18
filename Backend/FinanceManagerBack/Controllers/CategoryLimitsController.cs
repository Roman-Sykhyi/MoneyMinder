using FinanceManagerBack.Data.Category;
using FinanceManagerBack.Enums;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinanceManagerBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryLimitsController : ControllerBase
    {
        private readonly ICategoryLimitService _categoryLimitService;

        public CategoryLimitsController(ICategoryLimitService categoryLimitService)
        {
            _categoryLimitService = categoryLimitService;
        }

        [HttpGet]
        [Route("{walletId:int}/{categoryId:int}")]
        public int GetCategoryLimit(int walletId, int categoryId)
        {
            CategoryLimit categoryLimit = _categoryLimitService.GetCategoryLimit(walletId, categoryId);

            if (categoryLimit != null)
                return categoryLimit.Limit;
            else
                return 0;
        }
        
        [HttpPost]
        public void PostCategoryLimit(CategoryLimitDto categoryLimitDto)
        {
            _categoryLimitService.SendCategoryLimit(categoryLimitDto);
        }

        [HttpGet]
        [Route("checklimit/{walletId:int}/{categoryId:int}")]
        public ActionResult<LimitAlertEnum> CheckLimit(int walletId, int categoryId)
        {
            LimitAlertEnum result;

            try
            {
                result = _categoryLimitService.CheckLimit(walletId, categoryId);
            }
            catch(Exception)
            {
                return NoContent();
            }

            return result;
        }
    }
}
