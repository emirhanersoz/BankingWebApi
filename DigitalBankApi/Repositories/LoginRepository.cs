using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class LoginRepository : Repository<Logins>, ILoginRepository
    {
        public LoginRepository(AdminDbContext context) : base(context) { }

        public AdminDbContext AdminContext
        {
            get { return Context as AdminDbContext; }
        }
    }
}
