using FinanceManagerBack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagerBack.Interfaces
{
    public interface IRegularPaymentRepository
    {
        Task<IEnumerable<RegularPayment>> GetAll();
        Task<RegularPayment> GetById(int id);
        Task<RegularPayment> GetByName(string name);
        Task<IEnumerable<RegularPayment>> GetByWalletId(int id);
        Task<bool> Add(RegularPayment entity, int walletId);
        Task<bool> Delete(int id);
        Task Save();
    }
}
