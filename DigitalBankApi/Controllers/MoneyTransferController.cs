using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyTransferController : ControllerBase
    {
        private readonly MoneyTransferService _moneyTransferService;

        public MoneyTransferController(MoneyTransferService moneyTransferService)
        {
            _moneyTransferService = moneyTransferService;
        }

        [HttpPost("money-transfer"), Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        public async Task<IActionResult> TransferMoney(int senderAccountId, int recipientAccountId, decimal amount, string comment)
        {
            try
            {
                var isSuccess = await _moneyTransferService.TransferMoney(senderAccountId, recipientAccountId, amount, comment);

                if (isSuccess)
                {
                    return Ok("Money transfer successful.");
                }

                return NotFound("Money transfer failed.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
