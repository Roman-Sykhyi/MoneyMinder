using FinanceManagerBack.Data.Category;
using FinanceManagerBack.Data.Repositories;
using FinanceManagerBack.Enums;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace FinanceManagerBack.Tests.UnitTests
{
    public class CategoryLimitServiceTests
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
        public void CategoryLimitService_GetCategoryLimit()
        {
            var repository = new CategoryLimitRepository(_context);

            ICategoryLimitService service = new CategoryLimitService(repository, null);

            var result = service.GetCategoryLimit(2, 2);

            Assert.AreEqual(200, result.Limit);
        }

        [Test]
        [TestCase(3, 3, 300, 3)]
        [TestCase(2, 2, 0, 1)]
        public void CategoryLimitService_SendCategoryLimit(int categoryId, int walletId, int limit, int expected)
        {
            var categoryLimitDto = new CategoryLimitDto { CategoryId = categoryId, WalletId = walletId, Limit = limit };

            var repository = new CategoryLimitRepository(_context);

            var sut = new CategoryLimitService(repository, null);

            sut.SendCategoryLimit(categoryLimitDto);

            var categoryLimits = _context.CategoryLimits.ToList();

            Assert.AreEqual(expected, categoryLimits.Count);
        }

        [Test]
        public void CategoryLimitService_SendCategoryLimit_Update()
        {
            var categoryLimitDto = new CategoryLimitDto { CategoryId = 1, WalletId = 1, Limit = 1 };

            var repository = new CategoryLimitRepository(_context);

            var sut = new CategoryLimitService(repository, null);

            sut.SendCategoryLimit(categoryLimitDto);

            var categoryLimits = _context.CategoryLimits.ToList();

            Assert.AreEqual(1, categoryLimits.Find(p => p.WalletId == 1).Limit);
        }

        [Test]
        [TestCase(-50, 0)]
        [TestCase(-80, 1)]
        [TestCase(-120, 2)]
        public void CategoryLimitService_CheckLimit(int spent, int expected)
        {
            var repository = new CategoryLimitRepository(_context);

            var statisticsService = new Mock<StatisticsService>();

            statisticsService.Setup(s => s.GetStatisticsForPeriod("Month", 1, 1)).Returns(new int[] {spent, 50});

            var sut = new CategoryLimitService(repository, statisticsService.Object);

            LimitAlertEnum result = sut.CheckLimit(1, 1);

            Assert.AreEqual(expected, (int)result);
        }
    }
}