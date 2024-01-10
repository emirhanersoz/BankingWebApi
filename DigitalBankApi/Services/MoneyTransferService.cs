using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.DTOs;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Interfaces.IServices;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using FluentValidation;

namespace DigitalBankApi.Services
{
    public class MoneyTransferService : IMoneyTransferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<MoneyTransferService> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<MoneyTransferDto> _validator;
        private readonly AccountService _accountService;

        private static decimal dailyTransferLimit = 50000;
        private static decimal transactionLimit = 20000;

        public MoneyTransferService(AdminDbContext context, ILogger<MoneyTransferService> logger, IMapper mapper, IValidator<MoneyTransferDto> validator, AccountService accountService)
        {
            _unitOfWork = new UnitOfWork(context);
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
            _accountService = accountService;
        }

        public async Task<bool> TransferMoney(int senderAccountId, int recipientAccountId, decimal amount, string comment)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    if (amount <= 0)
                    {
                        _logger.LogError("Invalid transfer amount: {Amount}", amount);
                        return false;
                    }

                    if (!await _accountService.CheckAccount(senderAccountId))
                    {
                        _logger.LogError("Sender account not found.");
                        return false;
                    }

                    if (!await _accountService.CheckAccount(recipientAccountId))
                    {
                        _logger.LogError("Receiver account not found.");
                        return false;
                    }

                    if (!await CheckDailyTransferLimit(senderAccountId, amount))
                    {
                        _logger.LogError("Transfer limit exceeded.");
                        return false;
                    }

                    var senderAccount = await _unitOfWork.Accounts.GetAsync(senderAccountId);
                    var recipientAccount = await _unitOfWork.Accounts.GetAsync(recipientAccountId);

                    if (senderAccount.Balance < amount)
                    {
                        _logger.LogError("Balance not enough");
                        return false;
                    }

                    senderAccount.Balance -= amount;
                    recipientAccount.Balance += amount;

                    senderAccount.TotalDailyTransferAmount += amount;

                    var moneyTransferDto = new MoneyTransferDto
                    {
                        AccountId = senderAccountId,
                        DestAccountId = recipientAccountId,
                        Amount = amount,
                        Comment = comment,
                        TransactionDate = DateTime.Now.ToUniversalTime()
                    };

                    var result = _validator.Validate(moneyTransferDto);

                    if (!result.IsValid)
                    {
                        var errorMessages = result.Errors
                            .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                        throw new Exception(string.Join(Environment.NewLine, errorMessages));
                    }

                    var entity = _mapper.Map<MoneyTransferDto, MoneyTransfers>(moneyTransferDto);

                    await _unitOfWork.MoneyTransfers.AddAsync(entity);
                    await _unitOfWork.CompleteAsync();

                    transaction.Commit();

                    return true;
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, "An error occurred during the transfer.");

                    return false;
                }
            }
        }

        public async Task<bool> CheckDailyTransferLimit(int accountId, decimal amount)
        {
            decimal totalDailyTransfers = await GetTotalDailyTransfers(accountId, amount);

            if (totalDailyTransfers + amount > dailyTransferLimit || amount > transactionLimit)
            {
                _logger.LogError("Transfer limit exceeded. Limit: {Limit}, Used: {Used}, Requested: {Requested}",
                    dailyTransferLimit, totalDailyTransfers, amount);
                return false;
            }

            return true;
        }

        public async Task<decimal> GetTotalDailyTransfers(int accountId, decimal amount)
        {
            var moneyTransferAccount = await _unitOfWork.Accounts.GetAsync(accountId);

            if (moneyTransferAccount != null)
            {
                decimal totalDailyTransfers = moneyTransferAccount.TotalDailyTransferAmount + amount;
                return totalDailyTransfers;
            }

            else
            {
                return 0;
            }
        }
    }
}
