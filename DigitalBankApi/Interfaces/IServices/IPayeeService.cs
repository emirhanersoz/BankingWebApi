using DigitalBankApi.DTOs;
using DigitalBankApi.Enums;
using DigitalBankApi.Models;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface IPayeeService
    {
        Task<PayeeDto> Create(PayeeDto payeeDto);
        Task<List<PayeeDto>> ListPayeeForAccount(int accountId);
        Task<PayeeDto> Delete(int accountId, PayeeType payeeType);
        Task<PayeeDto> Payment(int accountId, PayeeType payeeType);
        void SetPaymentDate(Payees entity);
        Task<decimal> CalculateAllPayee(int accountId);
    }
}
