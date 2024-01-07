using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.Services;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface IMoneyTransferService
    {
        Task<bool> TransferMoney(int senderAccountId, int recipientAccountId, decimal amount, string comment);
        Task<bool> CheckDailyTransferLimit(int accountId, decimal amount);
        Task<decimal> GetTotalDailyTransfers(int accountId, decimal amount);
    }
}
