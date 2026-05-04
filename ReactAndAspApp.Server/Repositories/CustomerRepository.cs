using Microsoft.EntityFrameworkCore;
using ReactAndAspApp.Server.Data;
using ReactAndAspApp.Server.Models;

namespace ReactAndAspApp.Server.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext db) : base(db) { }

        public async Task<IEnumerable<Customer>> GetAllWithTypeAsync()
        {
            return await _db.Customers
                            .Include(c => c.CustomerType)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<Customer> GetWithTypeByIdAsync(int id)
        {
            return await _db.Customers
                            .Include(c => c.CustomerType)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
