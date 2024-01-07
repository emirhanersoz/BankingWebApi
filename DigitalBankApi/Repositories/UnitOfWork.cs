using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalBankApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdminContext _context;

        public UnitOfWork(AdminContext context)
        {
            _context = context;
            AccountCredits = new AccountCreditRepository(_context);
            Accounts = new AccountRepository(_context);
            Credits = new CreditRepository(_context);
            Customers = new CustomersRepository(_context);
            DepositWithdraws = new DepositWithdrawRepository(_context);
            Logins = new LoginRepository(_context);
            MoneyTransfers = new MoneyTransferRepository(_context);
            Payees = new PayeeRepository(_context);
            SupportRequests = new SupportRequestRepository(_context);
            Users = new UserRepository(_context);
        }

        public IAccountCreditRepository AccountCredits { get; private set; }
        public IAccountRepository Accounts { get; private set; }
        public ICreditRepository Credits { get; private set; }
        public ICustomersRepository Customers { get; private set; }
        public IDepositWithdrawRepository DepositWithdraws { get; private set; }
        public ILoginRepository Logins { get; private set; }
        public IMoneyTransferRepository MoneyTransfers { get; private set; }
        public IPayeeRepository Payees { get; private set; }
        public ISupportRequestRepository SupportRequests { get; private set; }
        public IUserRepository Users { get; private set; }
        public AdminContext Object { get; set; }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving changes to the database.", ex);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
