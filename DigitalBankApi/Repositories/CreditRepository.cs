using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class CreditRepository : Repository<Credits>, ICreditRepository
    {
        public CreditRepository(AdminContext context) : base(context) { }

        public AdminContext AdminContext
        {
            get { return Context as AdminContext; }
        }
    }
}
