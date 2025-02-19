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
        
        return customers;
    }
    
    public async Task<Result<Customer>> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (customer == null)
        {
            return Error.NotFound("Customer not found");
        }

        return customer;
    }
    
    public async Task<Result<Customer>> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            context.Customers.Add(customer);
            
            await context.SaveChangesAsync(cancellationToken);

            return customer;
        }
        catch
        {
            return Error.BadRequest("Couldn't create customer'");
        }
    }
    
    public async Task<Result<Customer>> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(new object[] { updatedCustomer.Id }, cancellationToken);

        if (customer == null)
        {
            return Error.NotFound("Customer not found");
        }

        customer.CustomerName = updatedCustomer.CustomerName;
        customer.Phone = updatedCustomer.Phone;

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return customer;
        }
        catch
        {
            return Error.BadRequest("Couldn't Update Customer");
        }
    }

    public async Task<Result<Deleted>> DeleteCustomerAsync(int id, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(new object[] { id }, cancellationToken);

        if (customer == null)
        {
            return Error.NotFound("Customer Not Found");
        }
        
        context.Customers.Remove(customer);
        await context.SaveChangesAsync(cancellationToken);
        return new Deleted();
        
    }
}
