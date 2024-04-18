using FinanceManagerBack.Dto.RegularPayment;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FinanceManagerBack.Tests.UnitTests
{
    public class RegularPaymentServiceTests
    {
        IRegularPaymentService _service;

        public RegularPaymentServiceTests()
        {
            _service = new RegularPaymentService();
        }

        private static IEnumerable<TestCaseData> UpdateWalletTestData
        {
            get
            {
                yield return new TestCaseData(new AddPaymentRequest() { Amount = 100, Date = DateTime.Now.Day, Name = "Salary", Period = 5 },
                new RegularPayment() { Amount = 100, Start = DateTime.Now, Name = "Salary", Period = 5 }
                );

                yield return new TestCaseData(new AddPaymentRequest() { Amount = 100, Date = DateTime.Now.Day - 4, Name = "Salary1", Period = 10 },
               new RegularPayment() { Amount = 100, Start = DateTime.Now.AddDays(-4).AddMonths(1), Name = "Salary1", Period = 10 }
               );

                yield return new TestCaseData(new AddPaymentRequest() { Amount = -100, Date = DateTime.Now.Day + 4, Name = "Salary2", Period = 10 },
               new RegularPayment() { Amount = -100, Start = DateTime.Now.AddDays(4), Name = "Salary2", Period = 10 });


            }
        }

        [Test]
        [TestCaseSource(nameof(UpdateWalletTestData))]
        public void CreatePayment(AddPaymentRequest request, RegularPayment expected)
        {
                var result = _service.Create(request);

                Assert.IsNotNull(result);

                Assert.AreEqual(expected, result);
        }

        [Test]
        public void CreatePayment_WrongInput()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Create(null));
        }

    }
}
