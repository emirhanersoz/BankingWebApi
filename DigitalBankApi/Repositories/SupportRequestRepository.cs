using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;

namespace DigitalBankApi.Repositories
{
    public class SupportRequestRepository : Repository<SupportRequests>, ISupportRequestRepository
    {
        public SupportRequestRepository(AdminDbContext context) : base(context) { }

        public AdminDbContext AdminContext
        {
            get { return Context as AdminDbContext; }
        }
    }
}
