﻿using AviApp.Interfaces;
using AviApp.Models;


namespace AviApp.Services;

public class OrderService : IOrderService
{
    private readonly List<Order> _orders = new List<Order>();

    public IEnumerable<Order> GetAllOrders() => _orders;

    public Order? GetOrderById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public Order CreateOrder(Order order)
    {
        order.Id = _orders.Count + 1; 
        _orders.Add(order);
        return order;
    }

    public Order? UpdateOrder(int id, Order updatedOrder)
    {
        var order = GetOrderById(id);
        if (order != null)
        {
            order.CustomerId = updatedOrder.CustomerId;
            order.Items = updatedOrder.Items;
            order.OrderDate = updatedOrder.OrderDate;
            return order;
        }
        return null;
    }

    public bool DeleteOrder(int id)
    {
        var order = GetOrderById(id);
        if (order != null)
        {
            _orders.Remove(order);
            return true;
        }
        return false;
    }
}