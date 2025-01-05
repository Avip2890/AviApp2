
using AviApp.Domain.Entities;

namespace AviApp.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Customer> CreateCustomer(Customer customer, CancellationToken cancellationToken = default);
    Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken = default);
    Task<bool> DeleteCustomerAsync(int id, CancellationToken cancellationToken = default);
}