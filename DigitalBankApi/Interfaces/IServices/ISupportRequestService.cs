using DigitalBankApi.DTOs;
using DigitalBankApi.Models;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface ISupportRequestService
    {
        Task<SupportRequestDto> Create(SupportRequestDto supportRequestDto);
        Task<List<SupportRequestDto>> ListCustomerSupportRequest(int customerId);
        Task<SupportRequests> ListSupportRequest(int supportRequestId);
        Task<AnswerRequestDto> FirstNotAnsweredRequest();
        Task<AnswerRequestDto> AnswerRequest(int id, string answer);
    }
}
