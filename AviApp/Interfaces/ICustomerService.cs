
using AviApp.Models;

namespace AviApp.Interfaces;

public interface ICustomerService
{
    // קבלת כל הלקוחות
    List<Customer> GetAllCustomers();
    
    // קבלת לקוח לפי מזהה
    Customer? GetCustomerById(int id);
    
    // יצירת לקוח חדש
    Customer CreateCustomer(Customer customer);
    
    // עדכון לקוח קיים
    Customer? UpdateCustomer(int id, Customer updatedCustomer);
    
    // מחיקת לקוח לפי מזהה
    bool DeleteCustomer(int id);
}