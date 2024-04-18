using FinanceManagerBack.Dto.Wallet;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FinanceManagerBack.Tests.UnitTests
{
    class WalletServiceTests
    {
        private readonly IWalletService walletService;

        public WalletServiceTests()
        {
            walletService = new WalletService();
        }

        private static IEnumerable<TestCaseData> UpdateWalletTestData
        {
            get
            {
                yield return new TestCaseData(new Wallet()
                {
                    Name = "Test1",
                    RegularPayments = new List<RegularPayment>()
                    {
                        new RegularPayment() { Amount = 1000, Start = DateTime.Now, Name = "Salary1", Period = 10 }
                    },
                    Transactions = new List<Transaction>(),
                    
                },
                new Wallet()
                {
                    Name = "Test1",
                    RegularPayments = new List<RegularPayment>()
                    {
                        new RegularPayment() { Amount = 1000, Start = DateTime.Now.AddDays(10), Name = "Salary1", Period = 10 }
                    },
                    Transactions = new List<Transaction>()
                    {
                        new Transaction{ CreationTime=DateTime.Now, Amount = 1000, IsRegular = true, Name = "Salary1"}
                    },
                    
                });

                yield return new TestCaseData(new Wallet()
                {
                    Name = "Test2",
                    RegularPayments = new List<RegularPayment>()
                    {
                        new RegularPayment() { Amount = 1000, Start = DateTime.Now.AddDays(10), Name = "Salary2", Period = 10 }
                    },
                    Transactions = new List<Transaction>(),
                   
                },
                new Wallet()
                {
                    Name = "Test2",
                    RegularPayments = new List<RegularPayment>()
                    {
                        new RegularPayment() { Amount = 1000, Start = DateTime.Now.AddDays(10), Name = "Salary2", Period = 10 }
                    },
                    Transactions = new List<Transaction>()
                    {
                    },
                    
                });
                yield return new TestCaseData(new Wallet()
                {
                    Name = "Test3",
                    RegularPayments = new List<RegularPayment>()
                    {
                       new RegularPayment() { Amount = 100, Start = DateTime.Now, Name = "Salary1", Period = 1 },
                       new RegularPayment() { Amount = 200, Start = DateTime.Now, Name = "Salary2", Period = 2 },
                       new RegularPayment() { Amount = 300, Start = DateTime.Now, Name = "Salary3", Period = 3 }
                    },
                    Transactions = new List<Transaction>(),
                    
                },
                new Wallet()
                {
                    Name = "Test3",
                    RegularPayments = new List<RegularPayment>()
                    {
                       new RegularPayment() { Amount = 100, Start = DateTime.Now.AddDays(1), Name = "Salary1", Period = 1 },
                       new RegularPayment() { Amount = 200, Start = DateTime.Now.AddDays(2), Name = "Salary2", Period = 2 },
                       new RegularPayment() { Amount = 300, Start = DateTime.Now.AddDays(3), Name = "Salary3", Period = 3 }
                    },
                    Transactions = new List<Transaction>()
                    {
                        new Transaction{ CreationTime=DateTime.Now, Amount = 100, IsRegular = true, Name = "Salary1"},
                        new Transaction{ CreationTime=DateTime.Now, Amount = 200, IsRegular = true, Name = "Salary2"},
                        new Transaction{ CreationTime=DateTime.Now, Amount = 300, IsRegular = true, Name = "Salary3"}
                    },
                    
                });
            }
        }

        [Test, TestCaseSource(nameof(UpdateWalletTestData))]
        public void Update_TestCases(Wallet data, Wallet expected)
        {
            walletService.Update(data);

            Assert.AreEqual(data, expected);
        }

        [Test]
        public void Update_WrongInput()
        {
            Assert.Throws<ArgumentNullException>(() => walletService.Update(null));
        }

        [Test]
        public void CountTotalSum_SingleTransaction_ReturnsTransactionAmount() {

            Wallet wallet=new Wallet() 
            { 
                Id = 1,
                Name="WalletTestData",
                Transactions = new List<Transaction>() 
                { 
                    new Transaction() 
                    { 
                        Amount=1
                    } 
                } 
            };

            WalletService service=new WalletService();

            decimal result = service.CountTotalSum(wallet);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void CountTotalSum_NoTransactions_ReturnsZero()
        {

            Wallet wallet = new Wallet()
            {
                Id = 1,
                Name = "WalletTestData",
                Transactions = new List<Transaction>()
            };

            WalletService service = new WalletService();

            decimal result = service.CountTotalSum(wallet);

            Assert.AreEqual(result, 0);
        }

        [Test]
        [TestCase(0,1,1)]
        [TestCase(0, 0, 0)]
        [TestCase(1, 1, 2)]
        [TestCase(-1, 0,-1)]
        public void CountTotalSum_NumerousTransactions_ReturnsTransactionsSum(decimal firstTransactionAmount, decimal secondTransactionAmount, decimal expected)
        {

            Wallet wallet = new Wallet()
            {
                Id = 1,
                Name = "WalletTestData",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = firstTransactionAmount
                    },
                    new Transaction()
                    {
                        Amount = secondTransactionAmount
                    }
                }
            };

            WalletService service = new WalletService();

            decimal result = service.CountTotalSum(wallet);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void CountTotalSum_MaximumSumResult_ThrowsException()
        {

            Wallet wallet = new Wallet()
            {
                Id = 1,
                Name = "WalletTestData",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = decimal.MaxValue
                    },
                    new Transaction()
                    {
                        Amount = 1
                    }
                }
            };

            WalletService service = new WalletService();

            TestDelegate result = ()=> service.CountTotalSum(wallet);

            Assert.Catch<OverflowException>(result);
        }

        [Test]
        public void CreateWallet_SingleWalletDto_WalletWithWalletDtoFields() 
        {
            WalletDto walletDto = new WalletDto() { Id = 1, Name = "TestWallet", TotalSum = 1 };
            Wallet expected = new Wallet() { Id = 1, Name = "TestWallet" };
            WalletService service = new WalletService();

            Wallet wallet = service.CreateWallet(walletDto);
            bool result = wallet.Equals(expected);

            Assert.IsTrue(result);
        }

        [Test]
        public void CreateWalletDto_SingleWallet_WalletDtoWithWalletFields()
        {

            Wallet wallet = new Wallet()
            {
                Id = 1,
                Name = "TestWallet",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount=1
                    }
                }
            };

            WalletDto expected = new WalletDto() { Id = 1, Name = "TestWallet",TotalSum=1 };
            WalletService service = new WalletService();

            WalletDto walletDto = service.CreateWalletDto(wallet,service.CountTotalSum);
            bool result = walletDto.Equals(expected);

            Assert.IsTrue(result);
        }

        [Test]
        public void CreateWalletsDto_NumerousWallets_NumerousWalletsDtoWithWalletFields()
        {
            IEnumerable<Wallet> wallets = new List<Wallet>() {
                new Wallet()
                {
                    Id = 1,
                    Name = "TestWallet1",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            Amount=1
                        }
                    }
                },
                new Wallet()
                {
                    Id = 2,
                    Name = "TestWallet2",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            Amount=1
                        }
                    }
                }
            };
            List<WalletDto> expected = new List<WalletDto>()
            { 
                new WalletDto() { Id = 1, Name = "TestWallet1", TotalSum = 1 }, 
                new WalletDto() { Id = 2, Name = "TestWallet2", TotalSum = 1 } 
            };
            WalletService service = new WalletService();
            const int firsWallet = 0;
            const int secondWallet = 1;


            var walletsDto = (List<WalletDto>)service.CreateWalletsDto(wallets);

            bool isAppropriateWalletsEquel = walletsDto[firsWallet].Equals(expected[firsWallet])
                && walletsDto[secondWallet].Equals( expected[secondWallet]);

            Assert.IsTrue(isAppropriateWalletsEquel);
        }
    }
}
