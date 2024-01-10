using DigitalBankApi.DTOs;
using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;

namespace DigitalBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportRequestController : ControllerBase
    {
        private readonly SupportRequestService _supportRequestService;

        public SupportRequestController(SupportRequestService supportRequestService)
        {
            _supportRequestService = supportRequestService;
        }

        [HttpPost, Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] SupportRequestDto supportRequest)
        {
            try
            {
                var createdSupportRequest = await _supportRequestService.Create(supportRequest);
                return Ok(createdSupportRequest);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin,Employee")]
        [Route("list-customer-support-request/{customerId}")]
        public async Task<IActionResult> ListCustomerSupportRequest(int customerId)
        {
            try
            {
                var listCustomerSupportRequest = await _supportRequestService.ListCustomerSupportRequest(customerId);
                return Ok(listCustomerSupportRequest);
            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin,Employee")]
        [Route("list-support-request/{supportRequestId}")]
        public async Task<IActionResult> ListSupportRequest(int supportRequestId)
        {
            try
            {
                var listSupportRequest = await _supportRequestService.ListSupportRequest(supportRequestId);
                return Ok(listSupportRequest);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin,Employee")]
        [Route("not-answered")]
        public async Task<IActionResult> FirstNotAnsweredRequest()
        {
            try
            {
                var firstNotAnsweredRequest = await _supportRequestService.FirstNotAnsweredRequest();
                return Ok(firstNotAnsweredRequest);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AnswerRequest(int id, string answer)
        {
            try
            {
                var answeredRequest = await _supportRequestService.AnswerRequest(id, answer);
                return Ok(answeredRequest);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
