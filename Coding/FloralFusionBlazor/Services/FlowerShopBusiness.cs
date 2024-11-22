using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using FlowerShopLibrary;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

public class FlowerShopBusiness
{
    private const string FIREBASE_PROJID = "flowerorderdatabase"; // Replace with your Firebase Project ID
    private FirestoreDb db;

    public FlowerShopBusiness()
    {
        try
        {
            InitFirestore(); // Initialize Firestore
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Firestore: {ex.Message}");
            throw; // Re-throw to handle higher up
        }
    }

    private void InitFirestore()
    {
        try
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                var filePath = "C:\\Users\\User\\source\\repos\\HarapanFF\\flowerorderdatabase-firebase-adminsdk-n6zxd-90aae6e66c.json";
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(filePath)
                });
            }

            db = FirestoreDb.Create(FIREBASE_PROJID);
            Console.WriteLine("Firestore client created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during Firestore initialization: {ex.Message}");
            throw;
        }
    }

    public async Task AddOrder(Order order)
    {
        DocumentReference docRef = db.Collection("orders").Document(); // Generate Firestore document ID
        string generatedId = docRef.Id;

        await docRef.SetAsync(new Dictionary<string, object>
        {
            { "GeneratedID", generatedId },
            { "Customer", new Dictionary<string, object>
                {
                    { "CustomerID", order.Customer.CustomerID },
                    { "Name", order.Customer.Name },
                    { "Address", order.Customer.Address },
                    { "TelephoneNumber", order.Customer.TelephoneNumber }
                }
            },
            { "Arrangements", order.Arrangements.Select(a => new Dictionary<string, object>
                {
                    { "Size", a.Size.ToString() },
                    { "Flowers", a.Flowers.Select(f => new Dictionary<string, object>
                        {
                            { "Name", f.Name },
                            { "Price", f.Price.ToString() },
                            { "ProductID", f.ProductID.ToString() }
                        })
                    }
                })
            },
            { "Status", order.Status.ToString() },
            { "PersonalizedMessage", order.PersonalizedMessage }
        });

        Console.WriteLine($"Order saved with Firestore ID: {generatedId}");
    }

    public async Task<List<Order>> GetAllOrders()
    {
        Query ordersQuery = db.Collection("orders");
        QuerySnapshot querySnapshot = await ordersQuery.GetSnapshotAsync();

        List<Order> orders = new List<Order>();
        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            string generatedId = documentSnapshot.Id;

            var data = documentSnapshot.ToDictionary();
            if (!data.ContainsKey("Customer"))
            {
                Console.WriteLine($"Missing Customer data for document ID: {generatedId}");
                continue;
            }

            var customerData = (Dictionary<string, object>)data["Customer"];
            if (!customerData.ContainsKey("CustomerID") || !customerData.ContainsKey("Name") ||
                !customerData.ContainsKey("Address") || !customerData.ContainsKey("TelephoneNumber"))
            {
                Console.WriteLine($"Incomplete Customer data for document ID: {generatedId}");
                continue;
            }

            Customer customer = new Customer(
                int.Parse(customerData["CustomerID"].ToString()),
                customerData["Name"].ToString(),
                customerData["Address"].ToString(),
                customerData["TelephoneNumber"].ToString()
            );

            Order order = new Order
            {
                GeneratedID = generatedId,
                Customer = customer,
                Arrangements = data.ContainsKey("Arrangements") ? ParseArrangements((List<object>)data["Arrangements"]) : new List<FlowerArrangement>(),
                Status = Enum.Parse<OrderStatus>(data["Status"].ToString()),
                PersonalizedMessage = data.ContainsKey("PersonalizedMessage") ? data["PersonalizedMessage"].ToString() : null
            };

            orders.Add(order);
        }

        return orders;
    }

    private List<FlowerArrangement> ParseArrangements(List<object> arrangementsData)
    {
        var arrangements = new List<FlowerArrangement>();
        foreach (Dictionary<string, object> arrangementData in arrangementsData)
        {
            Size size = Enum.Parse<Size>(arrangementData["Size"].ToString());
            var flowers = ParseFlowers((List<object>)arrangementData["Flowers"]);

            var arrangement = new FlowerArrangement(size);
            arrangement.Flowers.AddRange(flowers);
            arrangements.Add(arrangement);
        }

        return arrangements;
    }

    private List<FlowerProduct> ParseFlowers(List<object> flowersData)
    {
        var flowers = new List<FlowerProduct>();
        foreach (Dictionary<string, object> flowerData in flowersData)
        {
            flowers.Add(new FlowerProduct(
                flowerData["Name"].ToString(),
                decimal.Parse(flowerData["Price"].ToString()),
                int.Parse(flowerData["ProductID"].ToString())
            ));
        }
        return flowers;
    }

    public async Task DeleteOrder(string generatedId)
    {
        DocumentReference docRef = db.Collection("orders").Document(generatedId);
        await docRef.DeleteAsync();
        Console.WriteLine($"Order with ID {generatedId} deleted.");
    }
}

