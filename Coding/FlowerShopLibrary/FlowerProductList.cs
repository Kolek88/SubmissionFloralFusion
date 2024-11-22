using System.Text;

namespace FlowerShopLibrary
{
    public class FlowerProductList
    {
        public List<FlowerProduct> AvailableFlowers { get; private set; }

        public FlowerProductList()
        {
            AvailableFlowers = new List<FlowerProduct>
            {
                new FlowerProduct("Rose", 5.99m, 1),
                new FlowerProduct("Tulip", 4.99m, 2),
                new FlowerProduct("Sunflower", 3.99m, 3),
                new FlowerProduct("Lily", 6.99m, 4),
                new FlowerProduct("Daisy", 3.49m, 5),
                new FlowerProduct("Orchid", 8.99m, 6),
                new FlowerProduct("Carnation", 4.49m, 7),
                new FlowerProduct("Peony", 7.99m, 8),
                new FlowerProduct("Hydrangea", 6.49m, 9),
                new FlowerProduct("Chrysanthemum", 4.99m, 10),
                new FlowerProduct("Gerbera", 5.49m, 11),
                new FlowerProduct("Lavender", 3.99m, 12)
            };
        }

        // Helper method to display flower options for selection
        public string DisplayFlowerOptions()
        {
            var sb = new StringBuilder();
            foreach (var flower in AvailableFlowers)
            {
                sb.AppendLine($"{flower.ProductID}. {flower.Name} - {flower.GetFormattedPrice()}");
            }
            return sb.ToString();
        }

        // Get a single flower by Product ID
        public FlowerProduct GetFlowerProduct(int productID)
        {
            return AvailableFlowers.Find(p => p.ProductID == productID);
        }

        // Helper function to get multiple flowers by a list of IDs
        public List<FlowerProduct> GetFlowersByIDs(List<int> productIDs)
        {
            return AvailableFlowers.Where(f => productIDs.Contains(f.ProductID)).ToList();
        }
    }
}

