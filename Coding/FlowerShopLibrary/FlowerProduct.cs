using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary
{
	public class FlowerProduct
	{
       
        
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int ProductID { get; set; }

            public FlowerProduct(string name, decimal price, int productID)
            {
                Name = name;
                Price = price;
                ProductID = productID;
            }
        

        public string GetFormattedPrice()
		{
			return $"RM{Price:F2}";
		}
	}
}
