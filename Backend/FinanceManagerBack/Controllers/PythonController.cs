using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Python;
using FinanceManagerBack.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManagerBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PythonController : ControllerBase
    {
        IPythonScriptExecutor _pythonScriptExectutor;
        ICategoryLimitService _categoryLimitService;
        ICategoryService _categoryService;

        public PythonController(IPythonScriptExecutor pythonScriptExectutor, ICategoryLimitService categoryLimitService, ICategoryService categoryService)
        {
            _pythonScriptExectutor = pythonScriptExectutor;
            _categoryLimitService = categoryLimitService;
            _categoryService = categoryService;
        }

        [HttpGet("prediction/{categoryId}/{walletId}")]
        public IActionResult GetPrediction(int categoryId, int walletId)
        {
            var limit = _categoryLimitService.GetCategoryLimit(walletId, categoryId)?.Limit;
            var categoryName = _categoryService.GetCategoryById(categoryId)?.Name.ToLower();

            int index = categoryName.IndexOf(' ');
            categoryName = index >= 0 ? categoryName.Substring(0, index) : categoryName;

            var prediction = _pythonScriptExectutor.RunPredictionScript("car", -15200);

            string[] stringArray = prediction.Trim(new char[] { '[', ']'}).Replace("\n", "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<int> list = new List<int>();

            for (int i = 0; i < stringArray.Length; i++)
            {
                var dotIndex = stringArray[i].IndexOf('.');
                var test = stringArray[i].Substring(0, dotIndex);
                var value = int.Parse(test);
                list.Add(value);
            }

            list.Reverse();
            string result = string.Join(", ", list.Select(d => d.ToString()));

            return Ok(result);
        }
    }
}