using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Services;
using NUnit.Framework;
using System;

namespace FinanceManagerBack.Tests.UnitTests
{
    public class PeriodServiceTests
    {
        IPeriodService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new PeriodService();
        }

        [Test]
        public void PeriodService_All()
        {
            DateTime dateTime = _service.GetPeriod("All");

            Assert.AreEqual(DateTime.MinValue, dateTime);
        }

        [Test]
        public void PeriodService_Year()
        {
            DateTime dateTime = _service.GetPeriod("All");

            Assert.GreaterOrEqual(DateTime.Now.AddYears(-1), dateTime);
        }

        [Test]
        public void PeriodService_Month()
        {
            DateTime dateTime = _service.GetPeriod("All");

            Assert.GreaterOrEqual(DateTime.Now.AddMonths(-1), dateTime);
        }

        [Test]
        public void PeriodService_Week()
        {
            DateTime dateTime = _service.GetPeriod("All");

            Assert.GreaterOrEqual(DateTime.Now.AddDays(-7), dateTime);
        }

        [Test]
        public void PeriodService_WrongInput()
        {
            DateTime dateTime;

            Assert.Throws<ArgumentException>( () => dateTime = _service.GetPeriod("test"));
        }
    }
}