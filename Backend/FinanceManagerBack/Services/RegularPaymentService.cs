using FinanceManagerBack.Dto.RegularPayment;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using System;

namespace FinanceManagerBack.Services
{
    public class RegularPaymentService : IRegularPaymentService
    {
        public RegularPayment Create(AddPaymentRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var payment = new RegularPayment();

            payment.Amount = request.Amount;

            payment.Name = request.Name;

            payment.Period = request.Period;

            var day = request.Date - DateTime.Now.Day;

            if (day < 0)
                payment.Start = DateTime.Now.AddDays(day).AddMonths(1);
            else 
                payment.Start = DateTime.Now.AddDays(day);

            return payment; 
        }
    }
}