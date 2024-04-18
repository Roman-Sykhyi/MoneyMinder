using FinanceManagerBack.Enums;
using FinanceManagerBack.Interfaces;
using System;

namespace FinanceManagerBack.Services
{
    public class PeriodService : IPeriodService
    {
        public virtual DateTime GetPeriod(string periodStr)
        {
            Period period = Enum.Parse<Period>(periodStr);
            DateTime dateTime = DateTime.Now;

            switch (period)
            {
                case Period.All:
                    dateTime = DateTime.MinValue;
                    break;
                case Period.Year:
                    dateTime = dateTime.AddYears(-1);
                    break;
                case Period.Month:
                    dateTime = dateTime.AddMonths(-1);
                    break;
                case Period.Week:
                    dateTime = dateTime.AddDays(-7);
                    break;
            }

            return dateTime;
        }
    }
}
