using FinanceManagerBack.MethodExtensions;
using FinanceManagerBack.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManagerBack.Services
{
    public class CategoryService : ICategoryService
    {
        public static readonly Category[] DefaultCategories = new[]
        {
            new Category { Name = "Salary", Icon = "fa-solid fa-hand-holding-dollar", IsDefault = true},
            new Category { Name = "Other income", Icon = "fa-solid fa-coins", IsDefault = true},
            new Category { Name = "Products", Icon = "fa-solid fa-bowl-food", IsDefault = true},
            new Category { Name = "Transport", Icon = "fa-solid fa-bus-simple", IsDefault = true},
            new Category { Name = "Cafes / Restaurants", Icon = "fa-solid fa-utensils", IsDefault = true},
            new Category { Name = "Recreation / Entertainment", Icon = "fa-solid fa-beer-mug-empty", IsDefault = true},
            new Category { Name = "Health", Icon = "fa-solid fa-kit-medical", IsDefault = true},
            new Category { Name = "Gifts", Icon = "fa-solid fa-gift", IsDefault = true},
            new Category { Name = "Payments / Commissions", Icon = "fa-solid fa-money-check-dollar", IsDefault = true},
            new Category { Name = "Car", Icon = "fa-solid fa-car", IsDefault = true},
            new Category { Name = "House", Icon = "fa-solid fa-house", IsDefault = true},
            new Category { Name = "Communication / PC", Icon = "fa-solid fa-desktop", IsDefault = true},
            new Category { Name = "Shopping", Icon = "fa-solid fa-bag-shopping", IsDefault = true},
            new Category { Name = "Investment", Icon = "fa-solid fa-money-bill-trend-up", IsDefault = true},
        };

        private readonly ApplicationContext _context;

        public CategoryService(ApplicationContext context)
        {
            _context = context;

            AddDefaultCategoriesIfNotAdded();
        }

        public List<Category> GetDafultCategories()
        {
            return _context.Categories.Where(c => c.IsDefault).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        private void AddDefaultCategoriesIfNotAdded()
        {
            foreach (var category in DefaultCategories)
            {
                _context.Categories.AddIfNotExists(category, c => c.Name.Equals(category.Name));
            }

            _context.SaveChanges();
        }
    }
}