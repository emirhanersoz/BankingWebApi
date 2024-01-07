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
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AccountDto> _validator;
        private readonly IValidator<BalanceUpdateDto> _validatorUpdateDto;

        private const decimal MinimumBalanceAmount = 100;

        public AccountService(AdminContext context, IMapper mapper, IValidator<AccountDto> validator, IValidator<BalanceUpdateDto> validatorUpdateDto)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _validator = validator;
            _validatorUpdateDto = validatorUpdateDto;
        }

        public async Task<AccountDto> CreateAccount(AccountDto accountDto)
        {
            var result = _validator.Validate(accountDto);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            if (_unitOfWork.Customers.GetAsync(accountDto.CustomerId) == null)
            {
                throw new Exception("CustomerID not found.");
            }

            if (accountDto.Balance < MinimumBalanceAmount)
            {
                throw new Exception("Balance should be greater than or equal to the minimum balance amount.");
            }

            var entity = _mapper.Map<AccountDto, Accounts>(accountDto);

            await _unitOfWork.Accounts.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AccountDto>(accountDto);
        }

        public async Task<List<AccountDto>> ListAccounts(int customerId)
        {
            var allAccounts = await _unitOfWork.Accounts.GetAllAsync();

            var customerAccounts = allAccounts.Where(account => account.CustomerId == customerId).ToList();

            if (customerAccounts.Count == 0)
            {
                throw new Exception("Customer account not found.");
            }

            var accountDtos = _mapper.Map<List<AccountDto>>(customerAccounts);

            return accountDtos;
        }

        public async Task<BalanceUpdateDto> BalanceUpdate(int id, [FromBody] BalanceUpdateDto updatedBalance)
        {
            var checkingCustomer = await _unitOfWork.Customers.GetAsync(id);

            if (checkingCustomer == null)
            {
                throw new Exception($"Customer with ID {id} not found.");
            }

            var existingAccount = (await _unitOfWork.Accounts.FindAsync(account => account.CustomerId == id)).FirstOrDefault();

            if (existingAccount == null)
            {
                throw new Exception($"Account not found.");
            }

            _mapper.Map(updatedBalance, existingAccount);

            var result = _validatorUpdateDto.Validate(updatedBalance);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            await _unitOfWork.Accounts.UpdateAsync(existingAccount);
            await _unitOfWork.CompleteAsync();

            var updatedCustomerDto = _mapper.Map<BalanceUpdateDto>(existingAccount);

            return updatedCustomerDto;
        }

        public async Task<bool> CheckAccountId(int accountId)
        {
            var account = await _unitOfWork.Accounts.GetAsync(accountId);

            return account != null;
        }

        public async void ResetTotalDailyTransferLimit()
        {
            DateTime currentDate = DateTime.Now;
            DateTime nextDay = currentDate.AddDays(1);

            if (nextDay.Day == 1)
            {
                var allAccounts = await _unitOfWork.Accounts.GetAllAsync();

                foreach (var account in allAccounts)
                {
                    account.TotalDailyTransferAmount = 0;
                }

                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
