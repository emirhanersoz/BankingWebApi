using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.DTOs;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Interfaces.IServices;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace DigitalBankApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AccountDto> _validator;
        private readonly IValidator<UpdateBalanceDto> _updateBalanceValidator;

        private const decimal MinimumBalanceAmount = 1000;

        public AccountService(AdminDbContext context, IMapper mapper, IValidator<AccountDto> validator, IValidator<UpdateBalanceDto> updateBalanceValidator)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _validator = validator;
            _updateBalanceValidator = updateBalanceValidator;
        }

        public async Task<AccountDto> Create(AccountDto account)
        {
            var accountValid = _validator.Validate(account);

            if (!accountValid.IsValid)
            {
                var errorMessages = accountValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            if (_unitOfWork.Customers.GetAsync(account.CustomerId) == null)
            {
                throw new Exception("CustomerID not found.");
            }

            if (account.Balance < MinimumBalanceAmount)
            {
                throw new Exception("Balance should be greater than or equal to the minimum balance amount.");
            }

            var entity = _mapper.Map<AccountDto, Accounts>(account);

            await _unitOfWork.Accounts.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<List<AccountDto>> List(int customerId)
        {
            var listAll = await _unitOfWork.Accounts.GetAllAsync();

            var customerAccounts = listAll.Where(account => account.CustomerId == customerId).ToList();

            if (customerAccounts.Count == 0)
            {
                throw new Exception("Customer account not found.");
            }

            var accountDto = _mapper.Map<List<AccountDto>>(customerAccounts);

            return accountDto;
        }

        public async Task<UpdateBalanceDto> UpdateBalance(int id, [FromBody] UpdateBalanceDto updatedBalance)
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

            var updateBalanceValid = _updateBalanceValidator.Validate(updatedBalance);

            if (!updateBalanceValid.IsValid)
            {
                var errorMessages = updateBalanceValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            await _unitOfWork.Accounts.UpdateAsync(existingAccount);
            await _unitOfWork.CompleteAsync();

            var updatedCustomerDto = _mapper.Map<UpdateBalanceDto>(existingAccount);

            return updatedCustomerDto;
        }

        public async Task<bool> CheckAccount(int accountId)
        {
            var account = await _unitOfWork.Accounts.GetAsync(accountId);

            return account != null;
        }

        public async void ResetDailyTransferLimits()
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
