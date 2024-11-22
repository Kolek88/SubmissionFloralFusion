using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using FlowerShopLibrary;

public class OrderService
{
    private readonly List<Order> _orders = new();

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }

    public List<Order> GetAllOrders()
    {
        return _orders;
    }
}



