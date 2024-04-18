using FinanceManagerBack.Data.Category;
using FinanceManagerBack.Data.Repositories;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace FinanceManagerBack.Tests.UnitTests
{
    public class CategoryLimitRepositoryTests
    {
        ApplicationContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "Test")
               .Options;

            _context = new ApplicationContext(options);

            _context.CategoryLimits.Add(new CategoryLimit { CategoryId = 1, WalletId = 1, Limit = 100 });
            _context.CategoryLimits.Add(new CategoryLimit { CategoryId = 2, WalletId = 2, Limit = 200 });

            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var entity in _context.CategoryLimits)
                _context.CategoryLimits.Remove(entity);
            _context.SaveChanges();
        }

        [Test]
        public void CategoryLimitRepository_GetByWalletAndCategory()
        {
            ICategoryLimitRepository respository = new CategoryLimitRepository(_context);

            var result = respository.GetByWalletAndCategory(2, 2);

            var test = respository.GetByWalletAndCategory(3, 3);

            Assert.AreEqual(200, result.Limit);
        }

        [Test]
        public void CategoryLimitRepository_GetByWalletAndCategory_NullExpected()
        {
            ICategoryLimitRepository respository = new CategoryLimitRepository(_context);

            var result = respository.GetByWalletAndCategory(3, 3);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CategoryLimitRepository_Delete()
        {
            ICategoryLimitRepository repository = new CategoryLimitRepository(_context);

            var categoryLimits = _context.CategoryLimits.ToList();

            repository.Delete(categoryLimits[0]);

            categoryLimits = _context.CategoryLimits.ToList();

            Assert.AreEqual(1, categoryLimits.Count);
            Assert.AreEqual(200, categoryLimits[0].Limit);
        }

        [Test]
        public void CategoryLimitRepositroy_UpdateLimit()
        {
            ICategoryLimitRepository repository = new CategoryLimitRepository(_context);

            var categoryLimits = _context.CategoryLimits.ToList();

            repository.UpdateLimit(categoryLimits[0], 200);

            Assert.AreEqual(200, categoryLimits[0].Limit);
        }

        [Test]
        public void CategoryLimitRepositroy_AddNewLimit()
        {
            ICategoryLimitRepository repository = new CategoryLimitRepository(_context);

            repository.AddNewLimit( new CategoryLimitDto { CategoryId = 2, WalletId = 1, Limit = 100} );

            var categoryLimits = _context.CategoryLimits.ToList();

            Assert.AreEqual(3, categoryLimits.Count);
            Assert.AreEqual(100, categoryLimits[2].Limit);
        }
    }
}