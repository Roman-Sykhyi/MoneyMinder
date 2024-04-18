using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManagerBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("defaultCategories")]
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetDefaultCategories()
        {
            var categories = _categoryService.GetDafultCategories();

            if (!categories.Any())
                return NoContent();

            return Ok(categories);
        }
    }
}