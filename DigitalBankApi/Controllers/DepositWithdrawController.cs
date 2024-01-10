using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositWithdrawController : ControllerBase
    {
        private readonly DepositWithdrawService _depositWithdrawService;

        public DepositWithdrawController(DepositWithdrawService depositWithdrawService)
        {
            _depositWithdrawService = depositWithdrawService;
        }

        [HttpPost("withdraw"), Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        public async Task<IActionResult> Withdraw(int accountId, decimal withdrawAmount)
        {
            try
            {
                var isSuccess = await _depositWithdrawService.PerformWithdraw(accountId, withdrawAmount);

                if (isSuccess)
                {
                    return Ok("Withdrawal successful.");
                }

                return NotFound("Account not found or insufficient balance.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("high-withdraw"), Authorize(Roles = "Admin,Employee,HighLevelUser")]
        public async Task<IActionResult> HighWithdraw(int accountId, decimal withdrawAmount)
        {
            try
            {
                var isSuccess = await _depositWithdrawService.HighPerformWithdraw(accountId, withdrawAmount);

                if (isSuccess)
                {
                    return Ok("Withdrawal successful.");
                }

                return NotFound("Account not found or insufficient balance.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("deposit"), Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        public async Task<IActionResult> Deposit(int accountId, decimal depositAmount)
        {
            try
            {
                var isSuccess = await _depositWithdrawService.PerformDeposit(accountId, depositAmount);

                if (isSuccess)
                {
                    return Ok("Deposit successful.");
                }

                return NotFound("Account not found or insufficient balance.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("high-deposit"), Authorize(Roles = "Admin,Employee,HighLevelUser")]
        public async Task<IActionResult> HighDeposit(int accountId, decimal depositAmount)
        {
            try
            {
                var isSuccess = await _depositWithdrawService.HighPerformDeposit(accountId, depositAmount);

                if (isSuccess)
                {
                    return Ok("Deposit successful.");
                }

                return NotFound("Account not found or insufficient balance.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
