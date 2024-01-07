using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.Dtos;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using DigitalBankApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;
using System.Security.Principal;

namespace DigitalBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDto> _validator;
        private readonly CustomerService _customerService;

        public CustomerController(AdminContext context, IMapper mapper, IValidator<CustomerDto> validator, CustomerService customerService)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _validator = validator;
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
