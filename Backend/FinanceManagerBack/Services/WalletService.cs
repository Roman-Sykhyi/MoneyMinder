using FinanceManagerBack.Dto.Wallet;
using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagerBack.Services
{
    public class WalletService : IWalletService
    {
        public delegate decimal CountBalance(Wallet wallet);
        private readonly IWalletRepository _context;

        public WalletService()
        {

        }

        public WalletService(IWalletRepository context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WalletDto>> GetWalletsAsync(string email)
        {
            var wallets= await _context.GetWalletsAsync(email);
            return CreateWalletsDto(wallets);
        }

        public async Task<WalletDto> GetWalletAsync(int id, string email)
        {
            var wallet = await _context.GetWalletAsync(id, email);
            return CreateWalletDto(wallet, CountTotalSum);
        }

        public async Task AddWalletAsync(WalletDto walletDto,string email)
        {
            var wallet = CreateWallet(walletDto);
            await _context.AddWalletAsync(wallet,email);
            walletDto.Id=wallet.Id;
        }

        public async Task<WalletDto> DeleteWalletAsync(int id,string email)
        {
            var delitedWallet = await _context.DeleteWalletAsync(id,email);
            
            if(delitedWallet==null)
                return null;

            return CreateWalletDto(delitedWallet, CountTotalSum);
        }

        public WalletDto CreateWalletDto(Wallet wallet, CountBalance countBalance) {
            decimal totalSum= countBalance(wallet);
            return new WalletDto() {
                Id = wallet.Id,
                Name = wallet.Name,
                TotalSum = totalSum
            };
        }
        public IEnumerable<WalletDto> CreateWalletsDto(IEnumerable<Wallet> wallets) {
            
            var walletsDto=new List<WalletDto>();
            
            foreach (var wallet in wallets) {
                walletsDto.Add(CreateWalletDto(wallet, CountTotalSum));
            }

            return walletsDto;
        }

        public Wallet CreateWallet(WalletDto walletDto) {
            
            return new Wallet() { 
                Id = walletDto.Id,
                Name = walletDto.Name, 
            };
        }

        public decimal CountTotalSum(Wallet wallet) {
            decimal sum=0;
            foreach (var transaction in wallet.Transactions)
            {
                sum += transaction.Amount;
            }
            return sum;
        }
        public void Update(Wallet wallet)
        {
            if (wallet is null)
            {
                throw new ArgumentNullException(nameof(wallet));
            }

            foreach (var regularPayment in wallet.RegularPayments)
            {
                while (regularPayment.Start.Date <= DateTime.Now.Date)
                {
                    if (wallet.Transactions == null)
                        wallet.Transactions = new List<Transaction>();

                    wallet.Transactions.Add(CreateTransactionRegular(regularPayment.Name, regularPayment.Amount, regularPayment.Start));

                    regularPayment.Start = regularPayment.Start.AddDays(regularPayment.Period);
                }
            }

            wallet.Transactions.OrderBy(x => x.CreationTime);
        }

        public Transaction CreateTransactionRegular(string name, decimal amount, DateTime date)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            var transaction = new Transaction();

            transaction.CreationTime = date;

            transaction.IsRegular = true;

            transaction.Name = name;

            transaction.Amount = amount;

            return transaction;
        }

    }
}