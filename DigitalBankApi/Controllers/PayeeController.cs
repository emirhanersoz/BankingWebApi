using DigitalBankApi.DTOs;
using DigitalBankApi.Enums;
using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;

namespace DigitalBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayeeController : ControllerBase
    {
        private readonly PayeeService _payeeService;

        public PayeeController(PayeeService payeeService) 
        {
            _payeeService = payeeService;
        }

        [HttpPost]
        [Route("create"), Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        public async Task<IActionResult> Create([FromBody] PayeeDto payeeDto)
        {
            try
            {
                var createdPayee = await _payeeService.Create(payeeDto);
                return Ok(createdPayee);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("list/{accountId}"), Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        public async Task<IActionResult> ListAccountPayees(int accountId)
        {
            try
            {
                var payeeDto = await _payeeService.ListAccountPayees(accountId);
                return Ok(payeeDto);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("payment"), Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        public async Task<IActionResult> Payment(int accountId, PayeeType payeeType)
        {
            try
            {
                var payee = await _payeeService.Payment(accountId, payeeType);
                return Ok(payee);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{accountId}"), Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        public async Task<IActionResult> Delete(int accountId, PayeeType payeeType)
        {
            try
            {
                var payee = await _payeeService.Delete(accountId, payeeType);
                return Ok(payee);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
