using ReactAndAspApp.Server.Models;

namespace ReactAndAspApp.Server.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllWithTypeAsync();
        Task<Customer> GetWithTypeByIdAsync(int id);
    }
}
