using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManagerBack.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationContext _context;
        private readonly IPeriodService _periodService;

        public StatisticsService() { }

        public StatisticsService(ApplicationContext context, IPeriodService periodService)
        {
            _context = context;
            _periodService = periodService;
        }

        public virtual int[] GetStatisticsForPeriod(string periodStr, int walletId, int categoryId)
        {
            DateTime dateTime = _periodService.GetPeriod(periodStr);

           return CalculateStatistics(dateTime, walletId, categoryId);
        }

        private int[] CalculateStatistics(DateTime dateTime, int walletId, int categoryId)
        {
            int spending = 0, earnings = 0;

            List<Transaction> transactions;

            if(categoryId == -1)
                transactions = _context.Transactions.Where(t => t.CreationTime > dateTime && t.WalletId == walletId).ToList();
            else
                transactions = _context.Transactions.Where(t => t.CreationTime > dateTime && t.WalletId == walletId && t.Category.Id == categoryId).ToList();

            foreach (Transaction transaction in transactions)
            {
                if (transaction.Amount > 0)
                    earnings += (int)transaction.Amount;
                else
                    spending += (int)transaction.Amount;
            }

            return new int[] { spending, earnings };
        }
    }
}