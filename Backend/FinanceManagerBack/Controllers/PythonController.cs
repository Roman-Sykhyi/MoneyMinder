using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Python;
using FinanceManagerBack.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

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

        [HttpPost("ocr")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine(Environment.CurrentDirectory, file.FileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);

                    Thread.Sleep(5000);

                    var ocrResult = _pythonScriptExectutor.RunReceiptAnazyleScript(filePath);

                    Regex regex = new Regex(@"\d+(\.\d+)?");

                    Match match = regex.Match(ocrResult);

                    if (match.Success)
                    {
                        string numberString = match.Value;
                        if (double.TryParse(numberString, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                        {
                            return Ok(new { result });
                        }
                        else
                        {
                            return BadRequest("Failed to scan receipt");
                        }
                    }
                    else
                    {
                        return BadRequest("Failed to scan receipt");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}