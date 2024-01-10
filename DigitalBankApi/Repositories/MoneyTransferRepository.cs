using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class MoneyTransferRepository : Repository<MoneyTransfers>, IMoneyTransferRepository
    {
        public MoneyTransferRepository(AdminDbContext context) : base(context) { }

        public AdminDbContext AdminContext
        {
            get { return Context as AdminDbContext; }
        }
    }
}
