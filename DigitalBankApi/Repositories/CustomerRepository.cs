using DigitalBankApi.Data;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankApi.Repositories
{
    public class CustomersRepository : Repository<Customers>, ICustomersRepository
    {
        public CustomersRepository(AdminContext context) : base(context) { }

        public AdminContext AdminContext
        {
            get { return Context as AdminContext; }
        }
    }
}
