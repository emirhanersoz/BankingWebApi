using DigitalBankApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<AccountDto> Create(AccountDto account);
        Task<List<AccountDto>> List(int customerId);
        Task<UpdateBalanceDto> UpdateBalance(int id, [FromBody] UpdateBalanceDto updatedBalance);
        Task<bool> CheckAccount(int accountId);
        void ResetDailyTransferLimits();
    }
}
