using ReactAndAspApp.Server.Data;
using ReactAndAspApp.Server.Models;

namespace ReactAndAspApp.Server.Repositories
{
    public class CustomerTypeRepository : GenericRepository<CustomerType>, ICustomerTypeRepository
    {
        public CustomerTypeRepository(ApplicationDbContext db) : base(db) { }
    }
}
