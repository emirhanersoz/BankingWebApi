using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.Dtos;
using DigitalBankApi.DTOs;
using DigitalBankApi.Enums;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Interfaces.IServices;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using FluentValidation;

namespace DigitalBankApi.Services
{
    public class DepositWithdrawService : IDepositWithdrawService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<DepositWithdrawDto> _validator;

        private static readonly SemaphoreSlim transactionLock = new SemaphoreSlim(1, 1);

        public DepositWithdrawService(AdminContext context, IMapper mapper, IValidator<DepositWithdrawDto> validator)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<bool> PerformTransaction(int accountId, decimal amount, TransactionType transactionType)
        {
            await transactionLock.WaitAsync();

            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var account = await _unitOfWork.Accounts.GetAsync(accountId);

                        if (account == null)
                        {
                            await transaction.RollbackAsync();
                            return false;
                        }

                        if (transactionType == TransactionType.Withdraw && account.Balance < amount)
                        {
                            await transaction.RollbackAsync();
                            return false;
                        }

                        if (transactionType == TransactionType.Deposit)
                            account.Balance += amount;
                        else
                            account.Balance -= amount;

                        var transactionDto = new DepositWithdrawDto
                        {
                            AccountId = accountId,
                            Amount = amount,
                            TransactionType = transactionType,
                            isSucceded = true,
                            TransactionDate = DateTime.Now.ToUniversalTime()
                        };

                        var result = _validator.Validate(transactionDto);

                        if (!result.IsValid)
                        {
                            var errorMessages = result.Errors
                                .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                            throw new Exception(string.Join(Environment.NewLine, errorMessages));
                        }

                        var mappedTransaction = _mapper.Map<DepositWithdrawDto, DepositWithdraws>(transactionDto);

                        await _unitOfWork.DepositWithdraws.AddAsync(mappedTransaction);
                        await _unitOfWork.Accounts.UpdateAsync(account);
                        await _unitOfWork.CompleteAsync();
                        await transaction.CommitAsync();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }

            finally
            {
                transactionLock.Release();
            }
        }

        public async Task<bool> PerformDeposit(int accountId, decimal depositAmount)
        {
            return await PerformTransaction(accountId, depositAmount, TransactionType.Deposit);
        }

        public async Task<bool> PerformWithdraw(int accountId, decimal withdrawAmount)
        {
            return await PerformTransaction(accountId, withdrawAmount, TransactionType.Withdraw);
        }
    }
}
