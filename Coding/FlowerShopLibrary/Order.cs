using FlowerShopLibrary;
using System.Collections.Generic;
using System.Linq;

public class Order
{
    public string GeneratedID { get; set; }
    public Customer Customer { get; set; }
    public List<FlowerArrangement> Arrangements { get; set; } = new List<FlowerArrangement>();
    public OrderStatus Status { get; set; }
    public string PersonalizedMessage { get; set; }
    public int OrderID { get; internal set; }

    public Order()
    {
        Arrangements = new List<FlowerArrangement>();
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        Status = newStatus;
    }

    // Property to calculate the total price of the order
    public decimal TotalPrice => Arrangements.Sum(a => a.TotalPrice);
}

