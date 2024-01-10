using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalBankApi.Interfaces.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IAccountCreditRepository AccountCredits { get; }
        ICreditRepository Credits { get; }
        ICustomersRepository Customers { get; }
        IDepositWithdrawRepository DepositWithdraws { get; }
        ILoginRepository Logins { get; }
        IMoneyTransferRepository MoneyTransfers { get; }
        IPayeeRepository Payees { get; }
        ISupportRequestRepository SupportRequests { get; }
        IUserRepository Users { get; }

        IDbContextTransaction BeginTransaction();

        Task<int> CompleteAsync();
    }
}
