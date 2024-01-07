using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class AccountCreditRepository : Repository<AccountCredits>, IAccountCreditRepository
    {
        public AccountCreditRepository(AdminContext context) : base(context) { }

        public AdminContext AdminContext
        {
            get { return Context as AdminContext; }
        }
    }
}
