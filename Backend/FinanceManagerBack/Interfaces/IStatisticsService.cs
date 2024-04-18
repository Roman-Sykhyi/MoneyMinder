namespace FinanceManagerBack.Interfaces
{
    public interface IStatisticsService
    {
        public int[] GetStatisticsForPeriod(string period, int walletId, int categoryId);
    }
}
