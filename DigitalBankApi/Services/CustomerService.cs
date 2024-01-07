using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.Dtos;
using DigitalBankApi.DTOs;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Interfaces.IServices;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDto> _validator;

        public CustomerService(AdminContext context, IMapper mapper, IValidator<CustomerDto> validator)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CustomerDto> Create([FromBody] CustomerDto customer, string userId)
        {
            var result = _validator.Validate(customer);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            var entity = _mapper.Map<CustomerDto, Customers>(customer);
            entity.UserId = userId;

            await _unitOfWork.Customers.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> Update(int id, [FromBody] CustomerDto updatedCustomer)
        {
            var existingCustomer = await _unitOfWork.Customers.GetAsync(id);

            if (existingCustomer == null)
            {
                throw new Exception($"Customer with ID {id} not found.");
            }

            _mapper.Map(updatedCustomer, existingCustomer);

            var result = _validator.Validate(updatedCustomer);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            await _unitOfWork.Customers.UpdateAsync(existingCustomer);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CustomerDto>(existingCustomer);
        }

        public async Task<Customers> Delete(int id)
        {
            var existingCustomer = _unitOfWork.Customers.GetAsync(id);

            if (existingCustomer == null)
            {
                throw new Exception($"Customer with ID {id} not found.");
            }

            await _unitOfWork.Customers.RemoveAsync(await existingCustomer);
            await _unitOfWork.CompleteAsync();

            return await existingCustomer;
        }
    }
}
