using AviApp.Domain.Entities;
using AviApp.Results;

namespace AviApp.Interfaces;

public interface ICustomerService
{
    Task<Result<List<Customer>>> GetAllCustomersAsync(CancellationToken cancellationToken);
    Task<Result<Customer>> GetCustomerByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<Customer>> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
    Task<Result<Customer>> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteCustomerAsync(int id, CancellationToken cancellationToken);
}