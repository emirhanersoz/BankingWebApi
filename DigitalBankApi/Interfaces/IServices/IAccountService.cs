using DigitalBankApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccount(AccountDto accountDto);
        Task<List<AccountDto>> ListAccounts(int customerId);
        Task<BalanceUpdateDto> BalanceUpdate(int id, [FromBody] BalanceUpdateDto updatedBalance);
        Task<bool> CheckAccountId(int accountId);
        void ResetTotalDailyTransferLimit();
    }
}
