using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost, Authorize(Roles = "Admin,Empl")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreditDto creditDto)
        {
            try
            {
                var createdDto = await _creditService.Create(creditDto);
                return Ok(createdDto);
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
                var listAllCredits = await _creditService.ListAllCredits();
                return Ok(listAllCredits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin,Employee")]
        [Route("list-account-credit")]
        public async Task<IActionResult> ListAccountCredits(int accountId)
        {
            try
            {
                var listAccountCredit = await _creditService.ListAccountCredits(accountId);
                return Ok(listAccountCredit);
            }
            catch (Exception ex)
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
                var getCredit = await _creditService.GetHighCredit(accountId, creditId);
                return Ok(getCredit);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
