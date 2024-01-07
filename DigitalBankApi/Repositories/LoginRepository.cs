using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class LoginRepository : Repository<Logins>, ILoginRepository
    {
        public LoginRepository(AdminContext context) : base(context) { }

        public AdminContext AdminContext
        {
            get { return Context as AdminContext; }
        }
    }
}
