using DigitalBankApi.DTOs;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface ICreditService
    {
        Task<CreditDto> Create(CreditDto creditDto);
        Task<List<CreditDto>> ListAll();
        Task<List<CreditDto>> ListCreditsForAccount(int accountId);
        Task<AccountCreditDto> GetCredit(int accountId, int creditId);
        Task<AccountCreditDto> GetHighCredit(int accountId, int creditId);
    }
}
