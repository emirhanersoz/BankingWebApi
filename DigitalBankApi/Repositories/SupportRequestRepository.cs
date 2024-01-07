using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class SupportRequestRepository : Repository<SupportRequests>, ISupportRequestRepository
    {
        public SupportRequestRepository(AdminContext context) : base(context) { }

        public AdminContext AdminContext
        {
            get { return Context as AdminContext; }
        }
    }
}
