using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManagerBack.Tests.UnitTests
{
    public class CategoryServiceTests
    {
        [Test]
        public void CategoryServiceTest_14DefaultCategories()
        {
            var data = CategoryService.DefaultCategories.AsQueryable();

            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

            var service = new CategoryService(mockContext.Object);

            var defaultCategories = service.GetDafultCategories();

            Assert.AreEqual(CategoryService.DefaultCategories.Length, defaultCategories.Count);
        }

        [Test]
        public void CategoryServiceTest_0DefaultCategories()
        {
            var data = new List<Category>()
            {
                new Category { Id = 1, Name = "test", IsDefault = false}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

            var service = new CategoryService(mockContext.Object);

            var defaultCategories = service.GetDafultCategories();

            Assert.AreEqual(0, defaultCategories.Count);
        }
    }
}
