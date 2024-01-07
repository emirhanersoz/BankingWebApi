using DigitalBankApi.Enums;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface IDepositWithdrawService
    {
        Task<bool> PerformTransaction(int accountId, decimal amount, TransactionType transactionType);
        Task<bool> PerformDeposit(int accountId, decimal depositAmount);
        Task<bool> PerformWithdraw(int accountId, decimal withdrawAmount);
    }
}
