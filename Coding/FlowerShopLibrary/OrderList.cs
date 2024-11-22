using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary
{
	public class OrderList
	{
		private List<Order> Orders { get; set; }



		public OrderList()
		{
			Orders = new List<Order>();
		}

		public void AddOrder(Order order)
		{
			Orders.Add(order);
		}

		public Order GetOrder(int orderID)
		{
			return Orders.Find(o => o.OrderID == orderID);
		}

		public List<Order> GetOrdersByStatus(OrderStatus status)
		{
			return Orders.FindAll(o => o.Status == status);
		}

		public void UpdateOrderStatus(int orderID, OrderStatus newStatus)
		{
			Order order = GetOrder(orderID);
			if (order != null)
			{
				order.UpdateStatus(newStatus);
			}
		}

		public int GetOrderCount()
		{
			return Orders.Count;
		}
	}
}
