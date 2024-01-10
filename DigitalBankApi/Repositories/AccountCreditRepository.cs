using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class AccountCreditRepository : Repository<AccountCredits>, IAccountCreditRepository
    {
        public AccountCreditRepository(AdminDbContext context) : base(context) { }

        public AdminDbContext AdminContext
        {
            get { return Context as AdminDbContext; }
        }
    }
}
