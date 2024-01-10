using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.DTOs;
using DigitalBankApi.Enums;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Interfaces.IServices;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using FluentValidation;

namespace DigitalBankApi.Services
{
    public class PayeeService : IPayeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<PayeeDto> _validator;

        public PayeeService(AdminDbContext context, IMapper mapper, IValidator<PayeeDto> validator)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PayeeDto> Create(PayeeDto payee)
        {
            var payeeValid = _validator.Validate(payee);

            if (!payeeValid.IsValid)
            {
                var errorMessages = payeeValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }


            var entity = _mapper.Map<PayeeDto, Payees>(payee);

            SetPaymentDate(entity);

            await _unitOfWork.Payees.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PayeeDto>(entity);
        }

        public async Task<List<PayeeDto>> ListPayeeForAccount(int accountId)
        {
            if (await _unitOfWork.Accounts.GetAsync(accountId) == null)
            {
                throw new Exception("Account not found");
            }

            var allAccountPayees = await _unitOfWork.Payees.GetAllAsync();

            var accountPayees = allAccountPayees.Where(request => request.accountId == accountId).ToList();

            var payeesDto = _mapper.Map<List<PayeeDto>>(accountPayees);

            return payeesDto;
        }

        public async Task<PayeeDto> Delete(int accountId, PayeeType payeeType)
        {
            var account = await _unitOfWork.Accounts.GetAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            var payees = await _unitOfWork.Payees.GetAllAsync();

            var payeeToDelete = payees.FirstOrDefault(p => p.accountId == accountId && p.PayeeType == payeeType);

            if (payeeToDelete != null)
            {
                await _unitOfWork.Payees.RemoveAsync(payeeToDelete);
                await _unitOfWork.CompleteAsync();

                var entity = _mapper.Map<Payees, PayeeDto>(payees.FirstOrDefault());

                return entity;
            }

            else
            {
                throw new Exception("Payee not found for the specified account and payeeType");
            }
        }

        public async Task<PayeeDto> Payment(int accountId, PayeeType payeeType)
        {
            var account = await _unitOfWork.Accounts.GetAsync(accountId);

            if (account == null)
            {
                throw new Exception("Account not found");
            }

            var payees = await _unitOfWork.Payees.GetAllAsync();

            var payment = payees.FirstOrDefault(p => p.accountId == accountId && p.PayeeType == payeeType);

            if (payment == null)
            {
                throw new Exception("Payee not found");
            }

            if (account.Balance < payment.Amount)
            {
                throw new Exception("Balance not enough");
            }

            else
            {
                account.Balance = account.Balance - payment.Amount;
                payment.isPayment = true;
            }

            if (payment.PaymentDate >= DateTime.Now.ToUniversalTime())
            {
                account.Balance = account.Balance - payment.Amount;
                payment.isPayment = true;
            }

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PayeeDto>(payment);
        }

        public void SetPaymentDate(Payees entity)
        {
            entity.PaymentDate = DateTime.Now.ToUniversalTime();
            entity.PaymentDate = entity.PaymentDate.AddMonths(1);

            if (entity.PaymentDate.Day < entity.PaymentDay)
            {
                int addDays = entity.PaymentDay - entity.PaymentDate.Day;
                entity.PaymentDate = entity.PaymentDate.AddDays(addDays);
            }

            else
            {
                int addDays = entity.PaymentDate.Day - entity.PaymentDay;
                entity.PaymentDate = entity.PaymentDate.AddDays(-addDays);
            }
        }

        public async Task<decimal> CalculateAllPayee(int accountId)
        {
            decimal paymentsForAccount = 0;

            var account = await _unitOfWork.Accounts.GetAsync(accountId);

            if (account == null)
            {
                throw new Exception("Account not found");
            }

            var payees = await _unitOfWork.Payees.GetAllAsync();

            paymentsForAccount = payees.Where(p => p.accountId == accountId
                                && p.isPayment == false).Sum(p => p.Amount);

            return paymentsForAccount;
        }
    }
}
