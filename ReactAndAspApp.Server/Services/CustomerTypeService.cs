using ReactAndAspApp.Server.Models;
using ReactAndAspApp.Server.Repositories;

namespace ReactAndAspApp.Server.Services
{
    public class CustomerTypeService : ICustomerTypeService
    {
        private readonly ICustomerTypeRepository _customerTypeRepository;

        public CustomerTypeService(ICustomerTypeRepository customerTypeRepository)
        {
            _customerTypeRepository = customerTypeRepository;
        }

        public async Task<CustomerType> CreateAsync(CustomerType ct)
        {
            return await _customerTypeRepository.AddAsync(ct);
        }

        public async Task DeleteAsync(int id)
        {
            await _customerTypeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CustomerType>> GetAllAsync()
        {
            return await _customerTypeRepository.GetAllAsync();
        }

        public async Task<CustomerType> GetByIdAsync(int id)
        {
            return await _customerTypeRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, CustomerType ct)
        {
            var existing = await _customerTypeRepository.GetByIdAsync(id);
            if (existing == null) throw new ArgumentException("CustomerType not found");
            ct.Id = id;
            await _customerTypeRepository.UpdateAsync(ct);
        }
    }
}
