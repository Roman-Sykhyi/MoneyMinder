using FinanceManagerBack.Dto.Wallet;
using FinanceManagerBack.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagerBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        
        private readonly IWalletService _walletService;

        public WalletsController( IWalletService walletService)
        {
            _walletService = walletService;
        }

        // GET: api/Wallets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalletDto>>> GetWallets()
        {
            string email = GetUserEmail();
            var wallets = await _walletService.GetWalletsAsync(email);

            if (!wallets.Any())
                return NoContent();

            return Ok(wallets);
        }

        // GET: api/Wallets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WalletDto>> GetWallet(int id)
        {
            string email = GetUserEmail();
            var wallet = await _walletService.GetWalletAsync(id, email);

            if (wallet == null)
            {
                return NoContent();
            }

            return Ok(wallet);
        }

        // POST: api/Wallets
        [HttpPost]
        public async Task<ActionResult<WalletDto>> PostWallet(WalletDto wallet)
        {
            string email = GetUserEmail();
           
            await _walletService.AddWalletAsync(wallet, email);

            return CreatedAtAction("GetWallet", new { id = wallet.Id }, wallet);
        }

        // DELETE: api/Wallets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WalletDto>> DeleteWallet(int id)
        {
            string email = GetUserEmail();
            var wallet = await _walletService.DeleteWalletAsync(id,email);

            if (wallet == null)
            {
                return NoContent();
            }

            return wallet;
        }

        private string GetUserEmail() {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity != null) {
                var userClaims = identity.Claims;
                return userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
            }

            return null;
            
        }
    }
}