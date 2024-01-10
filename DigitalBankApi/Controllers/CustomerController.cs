using DigitalBankApi.Dtos;
using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;

namespace DigitalBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost, Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CustomerDto customer, string userId)
        {
            try
            {
                var createdCustomer = await _customerService.Create(customer, userId);
                return Ok(createdCustomer);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDto customer)
        {
            try
            {
                var updatedCustomer = await _customerService.Update(id, customer);
                return Ok(updatedCustomer);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedCustomer = await _customerService.Delete(id);
                return Ok(deletedCustomer);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
