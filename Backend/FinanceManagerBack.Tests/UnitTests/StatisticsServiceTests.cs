using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManagerBack.Tests.UnitTests
{
    public class StatisticsServiceTests
    {
        IQueryable<Transaction> data = new List<Transaction>
        {
            new Transaction { Amount = 100, CreationTime = DateTime.Now, Category = new Category { Id = 1 } },
            new Transaction { Amount = -100, CreationTime = DateTime.Now, Category = new Category { Id = 1 } },
            new Transaction { Amount = 100, CreationTime = DateTime.Now.AddDays(-10), Category = new Category { Id = -1 }  },
            new Transaction { Amount = -100, CreationTime = DateTime.Now.AddDays(-10), Category = new Category { Id = -1 }  },
            new Transaction { Amount = 100, CreationTime = DateTime.Now.AddDays(-40), Category = new Category { Id = -1 }  },
            new Transaction { Amount = -100, CreationTime = DateTime.Now.AddDays(-40), Category = new Category { Id = -1 }  },
            new Transaction { Amount = 100, CreationTime = DateTime.Now.AddYears(-2), Category = new Category { Id = -1 }  },
            new Transaction { Amount = -100, CreationTime = DateTime.Now.AddYears(-2), Category = new Category { Id = -1 }  }
        }.AsQueryable();

        Mock<DbSet<Transaction>> mockSet;
        Mock<ApplicationContext> mockContext;
        Mock<PeriodService> mockPeriodService;
        IStatisticsService service;

        [SetUp]
        public void SetUp()
        {
            mockSet = new Mock<DbSet<Transaction>>();
            mockSet.As<IQueryable<Transaction>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transaction>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transaction>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Transactions).Returns(mockSet.Object);

            mockPeriodService = new Mock<PeriodService>();
            mockPeriodService.Setup(p => p.GetPeriod("All")).Returns(DateTime.MinValue);
            mockPeriodService.Setup(p => p.GetPeriod("Year")).Returns(DateTime.Now.AddYears(-1));
            mockPeriodService.Setup(p => p.GetPeriod("Month")).Returns(DateTime.Now.AddMonths(-1));
            mockPeriodService.Setup(p => p.GetPeriod("Week")).Returns(DateTime.Now.AddDays(-7));
            mockPeriodService.Setup(p => p.GetPeriod("test")).Throws<Exception>();

            service = new StatisticsService(mockContext.Object, mockPeriodService.Object);
        }

        [Test]
        public void StatisticsServiceTest_PeriodAll()
        {
            int[] result = service.GetStatisticsForPeriod("All", 0, -1);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(-400, result[0]);
            Assert.AreEqual(400, result[1]);
        }

        [Test]
        public void StatisticsServiceTest_WithCategory()
        {
            int[] result = service.GetStatisticsForPeriod("All", 0, 1);

            Assert.AreEqual(-100, result[0]);
            Assert.AreEqual(100, result[1]);
        }

        [Test]
        public void StatisticsServiceTest_PeriodYear()
        {
            int[] result = service.GetStatisticsForPeriod("Year", 0, -1);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(-300, result[0]);
            Assert.AreEqual(300, result[1]);
        }

        [Test]
        public void StatisticsServiceTest_PeriodMonth()
        {
            int[] result = service.GetStatisticsForPeriod("Month", 0, -1);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(-200, result[0]);
            Assert.AreEqual(200, result[1]);
        }

        [Test]
        public void StatisticsServiceTest_PeriodWeek()
        {
            int[] result = service.GetStatisticsForPeriod("Week", 0, -1);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(-100, result[0]);
            Assert.AreEqual(100, result[1]);
        }

        [Test]
        public void StatisticsServiceTest_WrongPeriod()
        {
            Assert.Throws<Exception>(() => service.GetStatisticsForPeriod("test", 0, -1));
        }
    }
}