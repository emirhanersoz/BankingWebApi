using DigitalBankApi.DTOs;
using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;

namespace DigitalBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost, Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] AccountDto account)
        {
            try
            {
                var createdAccount = await _accountService.Create(account);
                return Ok(createdAccount);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        [Route("show-accounts-balance/{customerId}")]
        public async Task<IActionResult> List(int customerId)
        {
            try
            {
                var listAccount = await _accountService.List(customerId);
                return Ok(listAccount);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Authorize(Roles = "Admin,Employee")]
        [Route("update-balance/{id}")]
        public async Task<IActionResult> UpdateBalance(int id, [FromBody] UpdateBalanceDto updateBalance)
        {
            try
            {
                var updatedBalance = await _accountService.UpdateBalance(id, updateBalance);
                return Ok(updatedBalance);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
