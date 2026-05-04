using ReactAndAspApp.Server.Models;

namespace ReactAndAspApp.Server.Services
{
    public interface ICustomerTypeService
    {
        Task<IEnumerable<CustomerType>> GetAllAsync();
        Task<CustomerType> GetByIdAsync(int id);
        Task<CustomerType> CreateAsync(CustomerType ct);
        Task UpdateAsync(int id, CustomerType ct);
        Task DeleteAsync(int id);
    }
}
