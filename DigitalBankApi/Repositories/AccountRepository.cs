using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class AccountRepository : Repository<Accounts>, IAccountRepository
    {
        public AccountRepository(AdminDbContext context) : base(context) { }

        public AdminDbContext AdminContext
        {
            get { return Context as AdminDbContext; }
        }
    }
}
