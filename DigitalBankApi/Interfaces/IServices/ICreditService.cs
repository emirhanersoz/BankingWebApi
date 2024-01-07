using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.Services;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface ICreditService
    {
        Task<CreditDto> Create(CreditDto creditDto);
        Task<decimal> CalculateBankScore(int accountId);
        Task<bool> isLoanCredit(int accountId, int creditId);
        Task<List<CreditDto>> ListAllCredits();
        Task<List<CreditDto>> ListAccountCredits(int accountId);
        Task<AccountCreditDto> GetCredit(int accountId, int creditId);
        Task<AccountCreditDto> GetHighCredit(int accountId, int creditId);
    }
}
