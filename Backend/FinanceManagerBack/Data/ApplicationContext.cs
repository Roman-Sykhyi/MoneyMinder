using FinanceManagerBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FinanceManagerBack
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole, string>
    {
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;

        public virtual DbSet<RegularPayment> RegularPayments { get; set; } = null!;
        public virtual DbSet<CategoryLimit> CategoryLimits { get; set; } = null!;

        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    }
}
