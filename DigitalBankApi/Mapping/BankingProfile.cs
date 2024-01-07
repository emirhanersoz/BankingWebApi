using AutoMapper;
using DigitalBankApi.Dtos;
using DigitalBankApi.DTOs;
using DigitalBankApi.Models;

namespace DigitalBankApi.Mapping
{
    public class BankingProfile : Profile
    {
        public BankingProfile() 
        {
            CreateMap<AccountCreditDto, AccountCredits>();
            CreateMap<AnswerRequestDto, SupportRequests>();
            CreateMap<AccountDto, Accounts>();
            CreateMap<BalanceUpdateDto, Accounts>();
            CreateMap<CreditDto, Credits>();
            CreateMap<CustomerDto, Customers>();
            CreateMap<DepositWithdrawDto, DepositWithdraws>();
            CreateMap<LoginDto, Logins>(); 
            CreateMap<MoneyTransferDto, MoneyTransfers>();
            CreateMap<PayeeDto, Payees>();
            CreateMap<SupportRequestDto, SupportRequests>();
            CreateMap<UserDto, Users>();

            CreateMap<AccountCredits, AccountCreditDto>();
            CreateMap<Accounts, AccountDto>();
            CreateMap<Accounts, BalanceUpdateDto>();
            CreateMap<Credits, CreditDto>();
            CreateMap<Customers, CustomerDto>();
            CreateMap<DepositWithdraws, DepositWithdrawDto>();
            CreateMap<Logins, LoginDto>();
            CreateMap<MoneyTransfers, MoneyTransferDto>();
            CreateMap<Payees, PayeeDto>();
            CreateMap<SupportRequests, AnswerRequestDto>();
            CreateMap<SupportRequests, SupportRequestDto>();
            CreateMap<Users, UserDto>();
        }
    }
}
