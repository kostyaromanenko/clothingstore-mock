
namespace BYT_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Creating Product instances:");
            var product1 = new AccessoryItem("Hat", 19.99f, 100, "M", "Red", "CoolBrand");
            var product2 = new ClothingItem("T-Shirt", 29.99f, 50, "L", "Blue", "Cotton", "StyleBrand");

           
            Console.WriteLine("Saving Product extents...");
            Product.SaveExtent();

            
            Console.WriteLine("Loading Product extents...");
            Product.LoadExtent();

            Console.WriteLine("\nProducts loaded:");
            foreach (var product in Product.GetExtent())
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}");
            }

            
            Console.WriteLine("\nCreating Customer instance:");
            // var customer = new Customer("Alice", "alice@example.com", "123456789");

            
            Console.WriteLine("Saving Customer extent...");
            Customer.SaveExtent();

            
            Console.WriteLine("Loading Customer extent...");
            Customer.LoadExtent();

            Console.WriteLine("\nCustomers loaded:");
            foreach (var cust in Customer.GetExtent())
            {
                Console.WriteLine($"Customer ID: {cust.CustomerId}, Name: {cust.Name}");
            }

            
            Console.WriteLine("\nCreating Address instance:");
            var address = new Address("123 Main St", "Hometown", "12345");

            
            Console.WriteLine("Saving Address extent...");
            Address.SaveExtent();

            
            Console.WriteLine("Loading Address extent...");
            Address.LoadExtent();

            Console.WriteLine("\nAddresses loaded:");
            foreach (var addr in Address.GetExtent())
            {
                Console.WriteLine($"Address ID: {addr.AddressId}, Street: {addr.Street}");
            }

            
            Console.WriteLine("\nCreating Order instance:");
            var order = new Order(DateTime.Now);

            
            Console.WriteLine("Saving Order extent...");
            Order.SaveExtent();

            
            Console.WriteLine("Loading Order extent...");
            Order.LoadExtent();

            Console.WriteLine("\nOrders loaded:");
            foreach (var ord in Order.GetExtent())
            {
                Console.WriteLine($"Order ID: {ord.OrderId}, Date: {ord.Date}");
            }

            
            Console.WriteLine("\nCreating Store instance:");
            var store = new Store("Fashion Store", "Downtown", "9 AM - 9 PM");

            
            Console.WriteLine("Saving Store extent...");
            Store.SaveExtent();

            
            Console.WriteLine("Loading Store extent...");
            Store.LoadExtent();

            Console.WriteLine("\nStores loaded:");
            foreach (var str in Store.GetExtent())
            {
                Console.WriteLine($"Store ID: {str.StoreId}, Name: {str.StoreName}, Location: {str.StoreLocation}, Hours: {str.StoreHours}");
            }

            
            Console.WriteLine("\nCreating Employee instances:");
            var employee = new Employee("Bob", "Cashier", 3000, "USD");
            var manager = new Manager();
            var salesAssociate = new SalesAssociate();

            
            Console.WriteLine("Saving Employee extents...");
            Employee.SaveExtent();
            Manager.SaveExtent();
            SalesAssociate.SaveExtent();

            
            Console.WriteLine("Loading Employee extents...");
            Employee.LoadExtent();
            Manager.LoadExtent();
            SalesAssociate.LoadExtent();

            Console.WriteLine("\nEmployees loaded:");
            foreach (var emp in Employee.GetExtent())
            {
                Console.WriteLine($"Employee ID: {emp.EmployeeId}, Name: {emp.Name}, Currency: {emp.Currency ?? "Not Specified"}");
            }

            
            Console.WriteLine("\nCreating Payment instance:");
            var payment = new Payment(199.99f, "Credit Card");

            
            Console.WriteLine("Saving Payment extent...");
            Payment.SaveExtent();

            
            Console.WriteLine("Loading Payment extent...");
            Payment.LoadExtent();

            Console.WriteLine("\nPayments loaded:");
            foreach (var pay in Payment.GetExtent())
            {
                Console.WriteLine($"Payment ID: {pay.PaymentId}, Amount: {pay.Amount}");
            }

            
            Console.WriteLine("\nCreating Discount instance:");
            var discount = new Discount("SUMMER21", 15.0f, DateTime.Now.AddMonths(1));

            
            Console.WriteLine("Saving Discount extent...");
            Discount.SaveExtent();

            
            Console.WriteLine("Loading Discount extent...");
            Discount.LoadExtent();

            Console.WriteLine("\nDiscounts loaded:");
            foreach (var disc in Discount.GetExtent())
            {
                Console.WriteLine($"Discount Code: {disc.DiscountCode}, Percentage: {disc.Percentage}%, Expiry Date: {disc.ExpiryDate}");
            }

            
            Console.WriteLine("\nCreating Wishlist instance:");
            var wishlist = new Wishlist("Alice's Wishlist");
            wishlist.AddItem(product2 as ClothingItem);

            
            Console.WriteLine("Saving Wishlist extent...");
            Wishlist.SaveExtent();

            
            Console.WriteLine("Loading Wishlist extent...");
            Wishlist.LoadExtent();

            Console.WriteLine("\nWishlists loaded:");
            foreach (var wish in Wishlist.GetExtent())
            {
                Console.WriteLine($"Wishlist ID: {wish.WishlistId}, Name: {wish.Name}, Created Date: {wish.CreatedDate}, Items Count: {wish.Items.Count}");
            }

            
            Console.WriteLine("\nCreating Transaction instance:");
            var transaction = new Transaction(DateTime.Now, 150.75f);

            
            Console.WriteLine("Saving Transaction extent...");
            Transaction.SaveExtent();

            
            Console.WriteLine("Loading Transaction extent...");
            Transaction.LoadExtent();

            Console.WriteLine("\nTransactions loaded:");
            foreach (var trans in Transaction.GetExtent())
            {
                Console.WriteLine($"Transaction ID: {trans.TransactionId}, Date: {trans.TransactionDate}, Amount: {trans.Amount}");
            }
        }
    }
}
