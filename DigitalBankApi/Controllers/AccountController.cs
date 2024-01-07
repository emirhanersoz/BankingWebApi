using DigitalBankApi.DTOs;
using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;
using System.Data;

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
                var createdAccount = await _accountService.CreateAccount(account);
                return Ok(createdAccount);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin,Employee")]
        [Route("list/{customerId}")]
        public async Task<IActionResult> List(int customerId)
        {
            try
            {
                var accountDtos = await _accountService.ListAccounts(customerId);
                return Ok(accountDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> BalanceUpdate(int id, [FromBody] BalanceUpdateDto updatedBalance)
        {
            try
            {
                var result = await _accountService.BalanceUpdate(id, updatedBalance);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
