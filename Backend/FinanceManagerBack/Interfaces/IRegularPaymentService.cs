using FinanceManagerBack.Dto.RegularPayment;
using FinanceManagerBack.Models;

namespace FinanceManagerBack.Interfaces
{
    public interface IRegularPaymentService
    {
        RegularPayment Create(AddPaymentRequest request);
    }
}
