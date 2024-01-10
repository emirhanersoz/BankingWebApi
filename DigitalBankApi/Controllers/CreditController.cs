using DigitalBankApi.DTOs;
using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;

namespace DigitalBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly CreditService _creditService;

        public CreditController(CreditService creditService)
        {
            _creditService = creditService;
        }

        [HttpPost, Authorize(Roles = "Admin,Employee")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreditDto credit)
        {
            try
            {
                var createdCredit = await _creditService.Create(credit);
                return Ok(createdCredit);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        [Route("list-all")]
        public async Task<IActionResult> ListAll()
        {
            try
            {
                var listAllCredits = await _creditService.ListAll();
                return Ok(listAllCredits);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin,Employee")]
        [Route("list-account-credit")]
        public async Task<IActionResult> ListCreditsForAccount(int accountId)
        {
            try
            {
                var listCreditsForAccount = await _creditService.ListCreditsForAccount(accountId);
                return Ok(listCreditsForAccount);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Authorize(Roles = "Admin,Employee")]
        [Route("get-credit")]
        public async Task<IActionResult> GetCredit(int accountId, int creditId)
        {
            try
            {
                var getCredit = await _creditService.GetCredit(accountId, creditId);
                return Ok(getCredit);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Authorize(Roles = "Admin,Employee")]
        [Route("get-high-credit")]
        public async Task<IActionResult> GetHighCredit(int accountId, int creditId)
        {
            try
            {
                var getHighCredit = await _creditService.GetHighCredit(accountId, creditId);
                return Ok(getHighCredit);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
