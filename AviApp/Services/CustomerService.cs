
using AviApp.Interfaces;
using AviApp.Models;

namespace AviApp.Services;

public class CustomerService : ICustomerService
{
    // רשימה מדומה (in-memory) לשמירת הלקוחות
    private readonly List<Customer> _customers = new List<Customer>
    {
        new Customer { Id = 1, CustomerName = "John Doe", Phone = "123-456-7890" },
        new Customer { Id = 2, CustomerName = "Jane Smith", Phone = "987-654-3210" }
    };

    // קבלת כל הלקוחות
    public List<Customer> GetAllCustomers()
    {
        return _customers;
    }

    // קבלת לקוח לפי מזהה
    public Customer? GetCustomerById(int id)
    {
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    // יצירת לקוח חדש
    public Customer CreateCustomer(Customer customer)
    {
        customer.Id = _customers.Count + 1;
        _customers.Add(customer);
        return customer;
    }

    // עדכון לקוח קיים
    public Customer? UpdateCustomer(int id, Customer updatedCustomer)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return null;
        }

        customer.CustomerName = updatedCustomer.CustomerName;
        customer.Phone = updatedCustomer.Phone;
        return customer;
    }

    // מחיקת לקוח לפי מזהה
    public bool DeleteCustomer(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return false;
        }

        _customers.Remove(customer);
        return true;
    }
}