using AviApp.Domain.Context;
using AviApp.Interfaces;
using AviApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public class CustomerService(AvipAppDbContext context) : ICustomerService
{
    // קבלת כל הלקוחות
    public async Task<List<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
    {
        return await context.Customers.ToListAsync(cancellationToken);
    }

    // קבלת לקוח לפי מזהה
    public async Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Customers.FindAsync(new object[] { id }, cancellationToken);
    }

    // יצירת לקוח חדש
    public async Task<Customer> CreateCustomer(Customer customer, CancellationToken cancellationToken = default)
    {
        context.Customers.Add(customer);
        await context.SaveChangesAsync(cancellationToken);
        return customer;
    }

    // עדכון לקוח קיים
    public async Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken = default)
    {
        var customer = await context.Customers.FindAsync(new object[] { updatedCustomer.Id }, cancellationToken);
        if (customer == null)
        {
            return null;
        }

        customer.CustomerName = updatedCustomer.CustomerName;
        customer.Phone = updatedCustomer.Phone;

        await context.SaveChangesAsync(cancellationToken);
        return customer;
    }

    // מחיקת לקוח לפי מזהה
    public async Task<bool> DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await context.Customers.FindAsync(new object[] { id }, cancellationToken);
        if (customer == null)
        {
            return false;
        }

        context.Customers.Remove(customer);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}