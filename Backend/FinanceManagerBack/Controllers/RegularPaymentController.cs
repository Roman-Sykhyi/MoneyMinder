using FinanceManagerBack.Dto.RegularPayment;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagerBack.Controllers
{
    [Route("")]
    [ApiController]
    public class RegularPaymentController : ControllerBase
    {
        private readonly IRegularPaymentRepository repository;
        private readonly IRegularPaymentService service;

        public RegularPaymentController(IRegularPaymentRepository paymentRepository, IRegularPaymentService paymentService)
        {
            repository = paymentRepository;
            service = paymentService;
        }

        [HttpGet("payments")]
        public async Task<ActionResult<IEnumerable<RegularPayment>>> GetAll()
        {
            var payments = await repository.GetAll();

            if (payments != null)
            {
                return new ObjectResult(payments);
            }

            return NoContent();
        }


        [HttpGet("payments/{id}")]
        public async Task<ActionResult<RegularPayment>> Get(int id)
        {
            var payment = await repository.GetById(id);

            if (payment != null)
                return new ObjectResult(payment);

            return NoContent();
        }


        [HttpGet("paymentwallet/{walletID}")]
        public async Task<ActionResult<IEnumerable<RegularPayment>>> GetAllInWallet(int walletID)
        {
            var payments = await repository.GetByWalletId(walletID);

            if (payments != null)
                return new ObjectResult(payments);

           return NoContent();
        }


        [HttpPost("addpayment")]
        public async Task<ActionResult> Add([FromBody] AddPaymentRequest request)
        {
            if (ModelState.IsValid)
            {
                var payment = service.Create(request);

                var res = await repository.Add(payment, request.WalletId);

                if (!res)
                {
                    return BadRequest();
                }

                await repository.Save();

                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete("deletepayment/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await repository.Delete(id);

            if (res)
            {
                await repository.Save();
                return Ok();
            }
                
            return BadRequest();
        }
    }
}
