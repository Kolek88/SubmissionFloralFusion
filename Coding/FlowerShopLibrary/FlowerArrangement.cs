using System;
using System.Collections.Generic;

namespace FlowerShopLibrary
{
    public class FlowerArrangement
    {
        public Size Size { get; set; }
        public List<FlowerProduct> Flowers { get; set; } = new List<FlowerProduct>();

        public FlowerArrangement(Size size)
        {
            Size = size;
        }

        public void AddFlower(FlowerProduct flower)
        {
            if (flower == null)
            {
                throw new ArgumentNullException(nameof(flower), "Flower cannot be null.");
            }
            if (Flowers.Count < (int)Size)
            {
                Flowers.Add(flower);
            }
            else
            {
                throw new InvalidOperationException($"Cannot add more flowers. Maximum capacity ({(int)Size}) reached.");
            }
        }

        // Property to calculate total price of the arrangement
        public decimal TotalPrice => Flowers.Sum(f => f.Price);
    }
}
