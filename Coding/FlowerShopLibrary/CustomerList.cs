using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary
{
	public class CustomerList
	{
		private List<Customer> Customers { get; set; }

		public CustomerList()
		{
			Customers = new List<Customer>();
		}

		public void AddCustomer(Customer customer)
		{
			Customers.Add(customer);
		}

		public Customer GetCustomer(int customerID)
		{
			return Customers.Find(c => c.CustomerID == customerID);
		}

		public int GetCustomerCount()
		{
			return Customers.Count;
		}
	}
}
