using AviApp.Domain.Context;
using AviApp.Interfaces;
using AviApp.Domain.Entities;
using AviApp.Results;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public class CustomerService(AvipAppDbContext context) : ICustomerService
{
    public async Task<Result<List<Customer>>> GetAllCustomersAsync(CancellationToken cancellationToken)
    {
        var customers = await context.Customers.AsNoTracking().ToListAsync(cancellationToken);

        if (!customers.Any())
        {
            return Result<List<Customer>>.Failure("No customers found.");
        }

        return Result<List<Customer>>.Success(customers);
    }
    
    public async Task<Result<Customer>> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (customer == null)
        {
            return Result<Customer>.Failure($"Customer with ID {id} not found.");
        }

        return Result<Customer>.Success(customer);
    }
    
    public async Task<Result<Customer>> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync(cancellationToken);

            return Result<Customer>.Success(customer);
        }
        catch (Exception ex)
        {
            return Result<Customer>.Failure($"Failed to create customer: {ex.Message}");
        }
    }
    
    public async Task<Result<Customer>> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(new object[] { updatedCustomer.Id }, cancellationToken);

        if (customer == null)
        {
            return Result<Customer>.Failure($"Customer with ID {updatedCustomer.Id} not found.");
        }

        customer.CustomerName = updatedCustomer.CustomerName;
        customer.Phone = updatedCustomer.Phone;

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return Result<Customer>.Success(customer);
        }
        catch (Exception ex)
        {
            return Result<Customer>.Failure($"Failed to update customer: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteCustomerAsync(int id, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(new object[] { id }, cancellationToken);

        if (customer == null)
        {
            return Result<bool>.Failure($"Customer with ID {id} not found.");
        }

        try
        {
            context.Customers.Remove(customer);
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Failed to delete customer: {ex.Message}");
        }
    }
}
