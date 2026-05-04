using ReactAndAspApp.Server.Models;

namespace ReactAndAspApp.Server.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task UpdateAsync(int id, Customer customer);
        Task DeleteAsync(int id);
    }
}
