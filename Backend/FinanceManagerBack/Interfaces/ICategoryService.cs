using FinanceManagerBack.Models;
using System.Collections.Generic;

namespace FinanceManagerBack.Services
{
    public interface ICategoryService
    {
        public List<Category> GetDafultCategories();
        public Category GetCategoryById(int id);
    }
}