using System;

namespace FinanceManagerBack.Interfaces
{
    public interface IPeriodService
    {
        public DateTime GetPeriod(string periodStr);
    }
}
