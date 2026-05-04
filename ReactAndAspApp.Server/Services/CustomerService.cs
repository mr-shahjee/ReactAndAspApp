using ReactAndAspApp.Server.Models;
using ReactAndAspApp.Server.Repositories;

namespace ReactAndAspApp.Server.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            customer.LastUpdated = DateTime.UtcNow;
            return await _customerRepository.AddAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllWithTypeAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _customerRepository.GetWithTypeByIdAsync(id);
        }

        public async Task UpdateAsync(int id, Customer customer)
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing == null) throw new ArgumentException("Customer not found");

            // Update only scalar properties
            existing.Name = customer.Name;
            existing.Description = customer.Description;
            existing.Address = customer.Address;
            existing.City = customer.City;
            existing.State = customer.State;
            existing.Zip = customer.Zip;
            existing.CustomerTypeId = customer.CustomerTypeId;

            // Update LastUpdated
            existing.LastUpdated = DateTime.UtcNow;

            // Save changes
            await _customerRepository.UpdateAsync(existing);
        }
    }
}
