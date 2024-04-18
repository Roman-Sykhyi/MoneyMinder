using System;
using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagerBack.Interfaces;

namespace FinanceManagerBack.Tests.UnitTests
{
    internal class TransactionServiceTest
    {
        private ApplicationContext _context;
        private ITransactionService _service;

        private const int FirstWalletId = 1;
        private const int EmptyWalletId = 3;

        private readonly Transaction _transactionToAdd = new Transaction()
        { Amount = 20, CreationTime = DateTime.Now, Category = null, WalletId = 1 };

        readonly List<Transaction> _defaultTransactions = new List<Transaction>
        {
            new Transaction { Amount =  100, CreationTime = DateTime.Now, Category = null, WalletId = 1},
            new Transaction { Amount = -100, CreationTime = DateTime.Now, Category = null, WalletId = 1},

            new Transaction { Amount =  100, CreationTime = DateTime.Now.AddDays(-10), Category = null, WalletId = 2  },
            new Transaction { Amount = -100, CreationTime = DateTime.Now.AddDays(-10), Category = null, WalletId = 2  },
            new Transaction { Amount =  100, CreationTime = DateTime.Now.AddDays(-40), Category = null, WalletId = 2  },
            new Transaction { Amount = -100, CreationTime = DateTime.Now.AddDays(-40), Category = null, WalletId = 2  },
            new Transaction { Amount =  100, CreationTime = DateTime.Now.AddYears(-2), Category = null, WalletId = 2  },
            new Transaction { Amount = -100, CreationTime = DateTime.Now.AddYears(-2), Category = null, WalletId = 2  }
        };

        private readonly List<Wallet> _defaultWallets = new List<Wallet>
        {
            new Wallet {Id = 1, Name = "wallet1", CategoryLimits = null, RegularPayments = null},
            new Wallet {Id = 2, Name = "wallet2", CategoryLimits = null, RegularPayments = null},
            new Wallet {Id = 3, Name = "wallet3", CategoryLimits = null, RegularPayments = null}
        };

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            _context = new ApplicationContext(options);

            _context.Wallets.AddRange(_defaultWallets);
            _context.Transactions.AddRange(_defaultTransactions);

            _context.SaveChanges();
            _service = new TransactionService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var entity in _context.Transactions)
                _context.Transactions.Remove(entity);
            _context.SaveChanges();

            foreach (var entity in _context.Wallets)
                _context.Wallets.Remove(entity);
            _context.SaveChanges();
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public async Task TransactionServiceTest_DeleteTransaction(int idInDb)
        {
            var expected = _defaultTransactions[idInDb - 1];
            var actual = await _service.DeleteTransactionAsync(idInDb);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(_defaultTransactions.Count() - 1, _context.Transactions.Count());
        }

        [Test]
        public async Task TransactionServiceTest_AddTransaction()
        {
            await _service.AddTransactionAsync(_transactionToAdd);

            Assert.Contains(_transactionToAdd, _context.Transactions.ToList());
        }

        [Test]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(100)]
        public async Task TransactionServiceTest_GetTransactionById(int idInDb)
        {
            Transaction expected = null;
            if (idInDb < _defaultTransactions.Count())
            {
                expected = _defaultTransactions[idInDb - 1];
            } 
            var actual = await _service.GetTransactionAsync(idInDb);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(FirstWalletId)]
        [TestCase(EmptyWalletId)]
        public async Task TransactionServiceTest_GetTransactionsFromWallet(int walletId)
        {
            var expected = _defaultTransactions.Where(t => t.WalletId == walletId).ToList();
            var actual = await _service.GetTransactionsAsync(walletId);

            Assert.AreEqual(expected, actual);
        }
    }
}
