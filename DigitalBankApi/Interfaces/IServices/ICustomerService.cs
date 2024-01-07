using DigitalBankApi.Dtos;
using DigitalBankApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<CustomerDto> Create([FromBody] CustomerDto customer, string userId);
        Task<CustomerDto> Update(int id, [FromBody] CustomerDto customer);
        Task<Customers> Delete(int id);
    }
}
