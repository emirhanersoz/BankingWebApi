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
    public class CreditService : ICreditService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreditDto> _validator;
        private readonly PayeeService _payeeService;

        private static decimal highCreditAmount = 100000;

        public CreditService(AdminDbContext context, IMapper mapper, IValidator<CreditDto> validator, PayeeService payeeService)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _validator = validator;
            _payeeService = payeeService;
        }

        public async Task<CreditDto> Create(CreditDto credit)
        {
            var creditValid = _validator.Validate(credit);

            if (!creditValid.IsValid)
            {
                var errorMessages = creditValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            var entity = _mapper.Map<CreditDto, Credits>(credit);

            await _unitOfWork.Credits.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CreditDto>(entity);
        }

        public async Task<List<CreditDto>> ListAll()
        {
            var listAll = await _unitOfWork.Credits.GetAllAsync();

            return _mapper.Map<List<CreditDto>>(listAll);
        }

        public async Task<List<CreditDto>> ListCreditsForAccount(int accountId)
        {
            var allAccountCredits = await _unitOfWork.AccountCredits.GetAllAsync();

            if (!allAccountCredits.Where(p => p.AccountId == accountId).Any())
            {
                throw new ArgumentNullException("This account has no credit");
            }

            var creditDtos = new List<CreditDto>();

            foreach (var accountCredit in allAccountCredits.Where(account => account.AccountId == accountId))
            {
                var credit = await _unitOfWork.Credits.GetAsync(accountCredit.CreditId);
                var creditDto = _mapper.Map<CreditDto>(credit);
                creditDtos.Add(creditDto);
            }

            return creditDtos;
        }

        public async Task<AccountCreditDto> GetCredit(int accountId, int creditId)
        {
            bool isCreditApproved = await isLoanCredit(accountId, creditId);

            if (isCreditApproved)
            {
                var amount = await _unitOfWork.Credits.GetAsync(creditId);
                decimal creditAmount = amount.TotalAmount;

                if (creditAmount > highCreditAmount)
                {
                    throw new Exception("You can't withdraw large amounts of credit");
                }

                return await ProcessCredit(accountId, creditId);
            }

            else
            {
                var entity = await _unitOfWork.Accounts.GetAsync(accountId);
                throw new Exception("Credit not approved. You can take out a credit with a monthly payment of " + entity.BankScore);
            }
        }

        public async Task<AccountCreditDto> GetHighCredit(int accountId, int creditId)
        {
            bool isCreditApproved = await isLoanCredit(accountId, creditId);

            if (isCreditApproved)
            {
                return await ProcessCredit(accountId, creditId);
            }
            else
            {
                var entity = await _unitOfWork.Accounts.GetAsync(accountId);
                throw new Exception("Credit not approved. You can take out a credit with a monthly payment of " + entity.BankScore);
            }
        }

        private async Task<AccountCreditDto> ProcessCredit(int accountId, int creditId)
        {
            var accountCredit = new AccountCredits
            {
                AccountId = accountId,
                CreditId = creditId
            };

            await _unitOfWork.AccountCredits.AddAsync(accountCredit);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AccountCreditDto>(accountCredit);
        }

        private async Task<bool> isLoanCredit(int accountId, int creditId)
        {
            var credit = await _unitOfWork.Credits.GetAsync(creditId);
            var account = await _unitOfWork.Accounts.GetAsync(accountId);

            account.BankScore = await CalculateBankScore(accountId);

            if (account.BankScore >= credit.MontlyPayment)
            {
                return true;
            }

            return false;
        }

        private async Task<decimal> CalculateBankScore(int accountId)
        {
            var account = await _unitOfWork.Accounts.GetAsync(accountId);

            if (account != null)
            {
                decimal sumAllPayees = await _payeeService.CalculateAllPayee(accountId);

                decimal sumAllCredits = await SumAllCreditMonthlyPayment(accountId);

                decimal bankScore = (account.Balance / 2) - (sumAllPayees + sumAllCredits);

                return bankScore;
            }

            else
                throw new Exception("Calculate exception");
        }

        private async Task<decimal> SumAllCreditMonthlyPayment(int accountId)
        {
            decimal totalMonthlyPayments = 0;

            if (await HasCreditForAccount(accountId))
            {
                var allCredit = await ListCreditsForAccount(accountId);

                foreach (var credit in allCredit)
                {
                    totalMonthlyPayments += credit.MontlyPayment;
                }
            }

            return totalMonthlyPayments;
        }

        private async Task<bool> HasCreditForAccount(int accountId)
        {
            var allAccountCredits = await _unitOfWork.AccountCredits.GetAllAsync();

            if (!allAccountCredits.Where(p => p.AccountId == accountId).Any())
            {
                return false;
            }

            return true;
        }
    }
}
