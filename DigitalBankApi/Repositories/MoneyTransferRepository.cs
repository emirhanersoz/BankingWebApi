using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class MoneyTransferRepository : Repository<MoneyTransfers>, IMoneyTransferRepository
    {
        public MoneyTransferRepository(AdminContext context) : base(context) { }

        public AdminContext AdminContext
        {
            get { return Context as AdminContext; }
        }
    }
}
