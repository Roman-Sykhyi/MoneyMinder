using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FinanceManagerBack.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
    }
}