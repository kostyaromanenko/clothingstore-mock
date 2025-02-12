
using BYT_Project;

namespace BYT_TestProject
{
    public class AccessoryItemTest
    {
        [Test]
        public void TestConstructor()
        {
            string name = "Hat";
            float price = 19.99f;
            int stockQuantity = 100;
            string size = "M";
            string color = "Red";
            string brand = "CoolBrand";

            var accessoryItem = new AccessoryItem(name, price, stockQuantity, size, color, brand);

            Assert.AreEqual(name, accessoryItem.Name);
            Assert.AreEqual(price, accessoryItem.Price);
            Assert.AreEqual(stockQuantity, accessoryItem.StockQuantity);
            Assert.AreEqual(size, accessoryItem.Size);
            Assert.AreEqual(color, accessoryItem.Color);
            Assert.AreEqual(brand, accessoryItem.Brand);
        }

        [Test]
        public void TestSetSizeThrowException()
        {
            var accessoryItem = new AccessoryItem("Scarf", 9.99f, 50, "L", "Blue", "BrandX");

            Assert.Throws<ArgumentException>(() => accessoryItem.Size = null);
            Assert.Throws<ArgumentException>(() => accessoryItem.Size = "");

        }

        [Test]
        public void TestSetColorThrowException()
        {
            var accessoryItem = new AccessoryItem("Belt", 15.99f, 30, "M", "Green", "BrandY");

            Assert.Throws<ArgumentException>(() => accessoryItem.Color = null);
            Assert.Throws<ArgumentException>(() => accessoryItem.Color = "");

        }

        [Test]
        public void TestSetBrandThrowException()
        {
            var accessoryItem = new AccessoryItem("Gloves", 12.99f, 20, "S", "Black", "BrandZ");

            Assert.Throws<ArgumentException>(() => accessoryItem.Brand = null);
            Assert.Throws<ArgumentException>(() => accessoryItem.Brand = "");

        }

        [Test]
        public void TestGetSize()
        {
            var accessoryItem = new AccessoryItem("Scarf", 9.99f, 50, "L", "Blue", "BrandX");

            Assert.AreEqual("L", accessoryItem.Size);
        }

        [Test]
        public void TestGetColor()
        {
            var accessoryItem = new AccessoryItem("Belt", 15.99f, 30, "M", "Green", "BrandY");

            Assert.AreEqual("Green", accessoryItem.Color);
        }

        [Test]
        public void TestGetBrand()
        {
            var accessoryItem = new AccessoryItem("Gloves", 12.99f, 20, "S", "Black", "BrandZ");

            Assert.AreEqual("BrandZ", accessoryItem.Brand);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = AccessoryItem.GetExtent().Count;
            var accessoryItem = new AccessoryItem("Cap", 10.99f, 40, "L", "Yellow", "BrandA");

            var newCount = AccessoryItem.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_accessoryitems.xml";
            var accessoryItem = new AccessoryItem("Sunglasses", 29.99f, 15, "One Size", "Black", "BrandB");
            AccessoryItem.SaveExtent(path);

            AccessoryItem.LoadExtent(path);
            var loadedExtent = AccessoryItem.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(accessoryItem.Name, loadedExtent[^1].Name);
            Assert.AreEqual(accessoryItem.Price, loadedExtent[^1].Price);
            Assert.AreEqual(accessoryItem.StockQuantity, loadedExtent[^1].StockQuantity);
            Assert.AreEqual(accessoryItem.Size, loadedExtent[^1].Size);
            Assert.AreEqual(accessoryItem.Color, loadedExtent[^1].Color);
            Assert.AreEqual(accessoryItem.Brand, loadedExtent[^1].Brand);

            File.Delete(path);
        }
    }

    public class AddressTest
    {
        [Test]
        public void TestConstructor()
        {
            string street = "123 Main St";
            string city = "Springfield";
            string zipcode = "12345";

            var address = new Address(street, city, zipcode);

            Assert.AreEqual(street, address.Street);
            Assert.AreEqual(city, address.City);
            Assert.AreEqual(zipcode, address.Zipcode);
            Assert.IsTrue(address.AddressId > 0);
        }

        [Test]
        public void TestSetStreetThrowException()
        {
            var address = new Address("123 Main St", "Springfield", "12345");

            Assert.Throws<ArgumentException>(() => address.Street = null);
            Assert.Throws<ArgumentException>(() => address.Street = "");
        }

        [Test]
        public void TestSetCityThrowException()
        {
            var address = new Address("123 Main St", "Springfield", "12345");

            Assert.Throws<ArgumentException>(() => address.City = null);
            Assert.Throws<ArgumentException>(() => address.City = "");
        }

        [Test]
        public void TestSetZipcodeThrowException()
        {
            var address = new Address("123 Main St", "Springfield", "12345");

            Assert.Throws<ArgumentException>(() => address.Zipcode = null);
            Assert.Throws<ArgumentException>(() => address.Zipcode = "");
        }

        [Test]
        public void TestGetStreet()
        {
            var address = new Address("123 Main St", "Springfield", "12345");

            Assert.AreEqual("123 Main St", address.Street);
        }

        [Test]
        public void TestGetCity()
        {
            var address = new Address("123 Main St", "Springfield", "12345");

            Assert.AreEqual("Springfield", address.City);
        }

        [Test]
        public void TestGetZipcode()
        {
            var address = new Address("123 Main St", "Springfield", "12345");

            Assert.AreEqual("12345", address.Zipcode);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = Address.GetExtent().Count;

            var address = new Address("456 Elm St", "Shelbyville", "67890");

            var newCount = Address.GetExtent().Count;
            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_addresses.xml";
            var address = new Address("789 Oak St", "Capital City", "11223");
            Address.SaveExtent(path);

            Address.LoadExtent(path);
            var loadedExtent = Address.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(address.Street, loadedExtent[^1].Street);
            Assert.AreEqual(address.City, loadedExtent[^1].City);
            Assert.AreEqual(address.Zipcode, loadedExtent[^1].Zipcode);

            File.Delete(path);
        }
    }

    public class ClothingItemTest
    {
        [Test]
        public void TestConstructor()
        {
            string name = "T-Shirt";
            float price = 29.99f;
            int stockQuantity = 50;
            string size = "L";
            string color = "Blue";
            string fabric = "Cotton";
            string brand = "StyleBrand";

            var clothingItem = new ClothingItem(name, price, stockQuantity, size, color, fabric, brand);

            Assert.AreEqual(name, clothingItem.Name);
            Assert.AreEqual(price, clothingItem.Price);
            Assert.AreEqual(stockQuantity, clothingItem.StockQuantity);
            Assert.AreEqual(size, clothingItem.Size);
            Assert.AreEqual(color, clothingItem.Color);
            Assert.AreEqual(fabric, clothingItem.Fabric);
            Assert.AreEqual(brand, clothingItem.Brand);
        }

        [Test]
        public void TestSetSizeThrowException()
        {
            var clothingItem = new ClothingItem("Sweater", 39.99f, 30, "M", "Red", "Wool", "BrandA");

            Assert.Throws<ArgumentException>(() => clothingItem.Size = null);
            Assert.Throws<ArgumentException>(() => clothingItem.Size = "");
        }

        [Test]
        public void TestSetColorThrowException()
        {
            var clothingItem = new ClothingItem("Jacket", 59.99f, 20, "L", "Green", "Leather", "BrandB");

            Assert.Throws<ArgumentException>(() => clothingItem.Color = null);
            Assert.Throws<ArgumentException>(() => clothingItem.Color = "");
        }

        [Test]
        public void TestSetFabricThrowException()
        {
            var clothingItem = new ClothingItem("Pants", 49.99f, 40, "XL", "Black", "Denim", "BrandC");

            Assert.Throws<ArgumentException>(() => clothingItem.Fabric = null);
            Assert.Throws<ArgumentException>(() => clothingItem.Fabric = "");
        }

        [Test]
        public void TestSetBrandThrowException()
        {
            var clothingItem = new ClothingItem("Shirt", 19.99f, 100, "M", "Blue", "Cotton", "BrandD");

            Assert.Throws<ArgumentException>(() => clothingItem.Brand = null);
            Assert.Throws<ArgumentException>(() => clothingItem.Brand = "");
        }

        [Test]
        public void TestGetSize()
        {
            var clothingItem = new ClothingItem("Sweater", 39.99f, 30, "M", "Red", "Wool", "BrandA");

            Assert.AreEqual("M", clothingItem.Size);
        }

        [Test]
        public void TestGetColor()
        {
            var clothingItem = new ClothingItem("Jacket", 59.99f, 20, "L", "Green", "Leather", "BrandB");

            Assert.AreEqual("Green", clothingItem.Color);
        }

        [Test]
        public void TestGetFabric()
        {
            var clothingItem = new ClothingItem("Pants", 49.99f, 40, "XL", "Black", "Denim", "BrandC");

            Assert.AreEqual("Denim", clothingItem.Fabric);
        }

        [Test]
        public void TestGetBrand()
        {
            var clothingItem = new ClothingItem("Shirt", 19.99f, 100, "M", "Blue", "Cotton", "BrandD");

            Assert.AreEqual("BrandD", clothingItem.Brand);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = ClothingItem.GetExtent().Count;
            var clothingItem = new ClothingItem("T-Shirt", 29.99f, 50, "L", "Red", "Cotton", "BrandX");

            var newCount = ClothingItem.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_clothingitems.xml";
            var clothingItem = new ClothingItem("Jeans", 49.99f, 30, "M", "Blue", "Denim", "BrandY");
            ClothingItem.SaveExtent(path);

            ClothingItem.LoadExtent(path);
            var loadedExtent = ClothingItem.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(clothingItem.Name, loadedExtent[^1].Name);
            Assert.AreEqual(clothingItem.Price, loadedExtent[^1].Price);
            Assert.AreEqual(clothingItem.StockQuantity, loadedExtent[^1].StockQuantity);
            Assert.AreEqual(clothingItem.Size, loadedExtent[^1].Size);
            Assert.AreEqual(clothingItem.Color, loadedExtent[^1].Color);
            Assert.AreEqual(clothingItem.Fabric, loadedExtent[^1].Fabric);
            Assert.AreEqual(clothingItem.Brand, loadedExtent[^1].Brand);

            File.Delete(path);
        }
    }

 public class CustomerTests
    {
        [Test]
        public void TestConstructor()
        {
            string name = "Alice";
            string email = "alice@example.com";
            string phoneNumber = "123456789";
            var address = new Address("123 Street", "City", "Country");

            var customer = new Customer(name, email, phoneNumber, address);

            Assert.AreEqual(name, customer.Name);
            Assert.AreEqual(email, customer.Email);
            Assert.AreEqual(phoneNumber, customer.PhoneNumber);
            Assert.IsTrue(customer.CustomerId > 0);
            Assert.AreEqual(address, customer.Address);
        }

        [Test]
        public void TestSetNameThrowException()
        {
            var customer = new Customer("Bob", "bob@example.com", "987654321", new Address("123 Street", "City", "Country"));

            Assert.Throws<ArgumentException>(() => customer.Name = null);
            Assert.Throws<ArgumentException>(() => customer.Name = "");
        }

        [Test]
        public void TestSetEmailThrowException()
        {
            var customer = new Customer("Charlie", "charlie@example.com", "123123123", new Address("123 Street", "City", "Country"));

            Assert.Throws<ArgumentException>(() => customer.Email = null);
            Assert.Throws<ArgumentException>(() => customer.Email = "");
        }

        [Test]
        public void TestSetPhoneNumberThrowException()
        {
            var customer = new Customer("Dave", "dave@example.com", "321321321", new Address("123 Street", "City", "Country"));

            Assert.Throws<ArgumentException>(() => customer.PhoneNumber = null);
            Assert.Throws<ArgumentException>(() => customer.PhoneNumber = "");
        }

        [Test]
        public void TestAddOrder()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var order = new Order(DateTime.Now);  

            customer.AddOrder(order);

            Assert.Contains(order, customer.Orders.ToList());  
        }

        [Test]
        public void TestRemoveOrder()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var order = new Order(DateTime.Now);  

            customer.AddOrder(order);
            customer.RemoveOrder(order);

            Assert.IsFalse(customer.Orders.Contains(order));
        }

        [Test]
        public void TestUpdateOrder()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var oldOrder = new Order(DateTime.Now);
            var newOrder = new Order(DateTime.Now.AddDays(1));  

            customer.AddOrder(oldOrder);
            customer.UpdateOrder(oldOrder, newOrder);

            Assert.Contains(newOrder, customer.Orders.ToList());  
            Assert.IsFalse(customer.Orders.Contains(oldOrder));
        }

        [Test]
        public void TestAddWishlist()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var wishlist = new Wishlist("Electronics");

            customer.AddWishlist(wishlist);

            Assert.Contains(wishlist, customer.Wishlists.ToList());  
        }

        [Test]
        public void TestRemoveWishlist()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var wishlist = new Wishlist("Electronics");

            customer.AddWishlist(wishlist);
            customer.RemoveWishlist(wishlist);

            Assert.IsFalse(customer.Wishlists.Contains(wishlist));
        }

        [Test]
        public void TestUpdateWishlist()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var oldWishlist = new Wishlist("Electronics");
            var newWishlist = new Wishlist("Books");

            customer.AddWishlist(oldWishlist);
            customer.UpdateWishlist(oldWishlist, newWishlist);

            Assert.Contains(newWishlist, customer.Wishlists.ToList());  
            Assert.IsFalse(customer.Wishlists.Contains(oldWishlist));
        }

        [Test]
        public void TestAddTransaction()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var transaction = new Transaction(DateTime.Now, 100);  

            customer.AddTransaction(transaction);

            Assert.Contains(transaction, customer.Transactions.ToList());  
        }

        [Test]
        public void TestRemoveTransaction()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var transaction = new Transaction(DateTime.Now, 100);  

            customer.AddTransaction(transaction);
            customer.RemoveTransaction(transaction);

            Assert.IsFalse(customer.Transactions.Contains(transaction));
        }

        [Test]
        public void TestUpdateTransaction()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var oldTransaction = new Transaction(DateTime.Now, 100);  
            var newTransaction = new Transaction(DateTime.Now.AddDays(1), 200);

            customer.AddTransaction(oldTransaction);
            customer.UpdateTransaction(oldTransaction, newTransaction);

            Assert.Contains(newTransaction, customer.Transactions.ToList());  
            Assert.IsFalse(customer.Transactions.Contains(oldTransaction));
        }

        [Test]
        public void TestUpdateAddress()
        {
            var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
            var newAddress = new Address("456 Avenue", "NewCity", "NewCountry");

            customer.UpdateAddress(newAddress);

            Assert.AreEqual(newAddress, customer.Address);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = Customer.GetExtent().Count;

            var newCustomer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));

            var newCount = Customer.GetExtent().Count;
            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_customers.xml";
            var customer = new Customer("Frank", "frank@example.com", "777888999", new Address("123 Street", "City", "Country"));

            Customer.SaveExtent(path);
            Customer.LoadExtent(path);

            var loadedExtent = Customer.GetExtent();
            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(customer.Name, loadedExtent[^1].Name);
            Assert.AreEqual(customer.Email, loadedExtent[^1].Email);
            Assert.AreEqual(customer.PhoneNumber, loadedExtent[^1].PhoneNumber);

            File.Delete(path);
        }
    }

public class DiscountTest
{
    [Test]
    public void TestConstructor()
    {
        string discountCode = "SUMMER2024";
        float percentage = 20.5f;
        DateTime expiryDate = new DateTime(2024, 12, 31);

        var discount = new Discount(discountCode, percentage, expiryDate);

        Assert.AreEqual(discountCode, discount.DiscountCode);
        Assert.AreEqual(percentage, discount.Percentage);
        Assert.AreEqual(expiryDate, discount.ExpiryDate);
    }

    [Test]
    public void TestSetDiscountCodeThrowException()
    {
        var discount = new Discount("WINTER2024", 15f, DateTime.Now.AddMonths(1));

        Assert.Throws<ArgumentException>(() => discount.DiscountCode = null);
        Assert.Throws<ArgumentException>(() => discount.DiscountCode = "");
    }

    [Test]
    public void TestSetPercentageThrowException()
    {
        var discount = new Discount("SPRING2024", 10f, DateTime.Now.AddMonths(1));

        Assert.Throws<ArgumentException>(() => discount.Percentage = -5);
        Assert.Throws<ArgumentException>(() => discount.Percentage = 105);
    }

    [Test]
    public void TestGetDiscountCode()
    {
        var discount = new Discount("WINTER2024", 15f, DateTime.Now.AddMonths(1));

        Assert.AreEqual("WINTER2024", discount.DiscountCode);
    }

    [Test]
    public void TestGetPercentage()
    {
        var discount = new Discount("SUMMER2024", 20.5f, DateTime.Now.AddMonths(1));

        Assert.AreEqual(20.5f, discount.Percentage);
    }

    [Test]
    public void TestGetExpiryDate()
    {
        DateTime expiryDate = new DateTime(2024, 12, 31);
        var discount = new Discount("SUMMER2024", 20.5f, expiryDate);

        Assert.AreEqual(expiryDate, discount.ExpiryDate);
    }

    [Test]
    public void TestExtentIncreasing()
    {
        var initialCount = Discount.GetExtent().Count;
        var newDiscount = new Discount("BLACKFRIDAY2024", 30f, DateTime.Now.AddMonths(1));

        var newCount = Discount.GetExtent().Count;
        Assert.AreEqual(initialCount + 1, newCount);
    }

    [Test]
    public void TestSaveAndLoadExtent()
    {
        string path = "test_discounts.xml";
        var discount = new Discount("CYBER2024", 50f, DateTime.Now.AddMonths(1));

        Discount.SaveExtent(path);
        Discount.LoadExtent(path);

        var loadedExtent = Discount.GetExtent();
        Assert.IsNotEmpty(loadedExtent);
        Assert.IsTrue(File.Exists(path));
        Assert.AreEqual(discount.DiscountCode, loadedExtent[^1].DiscountCode);
        Assert.AreEqual(discount.Percentage, loadedExtent[^1].Percentage);
        Assert.AreEqual(discount.ExpiryDate, loadedExtent[^1].ExpiryDate);

        File.Delete(path);
    }
    
    [Test]
    public void TestSetOrder()
    {
        var order = new Order();
        var discount = new Discount("WINTER2024", 15f, DateTime.Now.AddMonths(1));

        discount.SetOrder(order);

        Assert.AreEqual(order.Discounts.Count, 1);
        Assert.AreEqual(order.Discounts[0], discount);
        Assert.AreEqual(discount.Order, order);
    }
    [Test]
    public void TestSetOrderReplaceExistingOrder()
    {
        var order1 = new Order();
        var order2 = new Order();
        var discount = new Discount("SPRING2024", 10f, DateTime.Now.AddMonths(1));

        discount.SetOrder(order1); 
        discount.SetOrder(order2); 

        Assert.AreEqual(order1.Discounts.Count, 0);  
        Assert.AreEqual(order2.Discounts.Count, 1);  
        Assert.AreEqual(order2.Discounts[0], discount);  
    }

    [Test]
    public void TestRemoveOrder()
    {
        var order = new Order();
        var discount = new Discount("SUMMER2024", 20f, DateTime.Now.AddMonths(1));

        discount.SetOrder(order);
        discount.RemoveOrder();

        Assert.AreEqual(order.Discounts.Count, 0);
        Assert.IsNull(discount.Order);
    }

}


    public class EmployeeTest
    {
        [SetUp]
        public void SetUp()
        {
            Employee.ClearExtent();
        }
        
        [Test]
        public void TestConstructor()
        {
            string name = "John Doe";
            string role = "Manager";
            float salary = 50000f;

            var employee = new Employee(name, role, salary);

            Assert.AreEqual(name, employee.Name);
            Assert.AreEqual(role, employee.Role);
            Assert.AreEqual(salary, employee.Salary);
            Assert.AreEqual(1, employee.EmployeeId);
        }

        [Test]
        public void TestSetNameThrowException()
        {
            var employee = new Employee("Jane Doe", "Sales Associate", 35000f);

            Assert.Throws<ArgumentException>(() => employee.Name = null);
            Assert.Throws<ArgumentException>(() => employee.Name = "");
        }

        [Test]
        public void TestSetRoleThrowException()
        {
            var employee = new Employee("John Smith", "Manager", 60000f);

            Assert.Throws<ArgumentException>(() => employee.Role = null);
            Assert.Throws<ArgumentException>(() => employee.Role = "");
        }

        [Test]
        public void TestSetSalaryThrowException()
        {
            var employee = new Employee("Alice Johnson", "Sales Associate", 45000f);

            Assert.Throws<ArgumentException>(() => employee.Salary = -500f);
        }

        [Test]
        public void TestGetName()
        {
            var employee = new Employee("Bob Brown", "Manager", 55000f);

            Assert.AreEqual("Bob Brown", employee.Name);
        }

        [Test]
        public void TestGetRole()
        {
            var employee = new Employee("Charlie Blue", "Security", 30000f);

            Assert.AreEqual("Security", employee.Role);
        }

        [Test]
        public void TestGetSalary()
        {
            var employee = new Employee("David Yellow", "Sales Associate", 35000f);

            Assert.AreEqual(35000f, employee.Salary);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = Employee.GetExtent().Count;
            var employee = new Employee("Eve Green", "Manager", 60000f);

            var newCount = Employee.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_employees.xml";
            var employee = new Employee("Frank Black", "Sales Associate", 38000f);
            Employee.SaveExtent(path);

            Employee.LoadExtent(path);
            var loadedExtent = Employee.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(employee.Name, loadedExtent[^1].Name);
            Assert.AreEqual(employee.Role, loadedExtent[^1].Role);
            Assert.AreEqual(employee.Salary, loadedExtent[^1].Salary);

            File.Delete(path);
        }

        [Test]
        public void TestLoadExtentWhenFileDoesNotExist()
        {
            string path = "non_existent_file.xml";

            var result = Employee.LoadExtent(path);

            Assert.IsFalse(result);
            Assert.AreEqual(0, Employee.GetExtent().Count);
        }
        
        [Test]
        public void TestAddSubordinate()
        {
            var supervisor = new Employee("Manager", "Manager", 60000f);
            var subordinate = new Employee("Employee", "Sales Associate", 35000f);
            
            supervisor.AddSubordinate(subordinate);
            
            Assert.Contains(subordinate, (System.Collections.ICollection)supervisor.Subordinates); 
            Assert.AreEqual(supervisor, subordinate.Supervisor); 
        }

        [Test]
        public void TestAddSubordinateThrowsExceptionForNull()
        {
            var employee = new Employee("Manager", "Manager", 60000f);

            Assert.Throws<ArgumentException>(() => employee.AddSubordinate(null));
        }

        [Test]
        public void TestAddSubordinateThrowsExceptionForSelf()
        {
            var employee = new Employee("Manager", "Manager", 60000f);

            Assert.Throws<InvalidOperationException>(() => employee.AddSubordinate(employee));
        }
        
        [Test]
        public void TestRemoveSubordinate()
        {
            var supervisor = new Employee("Manager", "Manager", 60000f);
            var subordinate = new Employee("Employee", "Sales Associate", 35000f);

            supervisor.AddSubordinate(subordinate);
            supervisor.RemoveSubordinate(subordinate);

            Assert.IsFalse(supervisor.Subordinates.Contains(subordinate));
            Assert.IsNull(subordinate.Supervisor);
        }

        [Test]
        public void TestRemoveSubordinateThrowsExceptionForNull()
        {
            var employee = new Employee("Manager", "Manager", 60000f);

            Assert.Throws<ArgumentException>(() => employee.RemoveSubordinate(null));
        }
        
        [Test]
        public void TestSetSupervisor()
        {
            var supervisor = new Employee("Manager", "Manager", 60000f);
            var subordinate = new Employee("Employee", "Sales Associate", 35000f);
            
            subordinate.SetSupervisor(supervisor);
            
            Assert.AreEqual(supervisor, subordinate.Supervisor); 
            Assert.Contains(subordinate, (System.Collections.ICollection)supervisor.Subordinates); 
        }

        [Test]
        public void TestSetSupervisorThrowsExceptionForSelf()
        {
            var employee = new Employee("Manager", "Manager", 60000f);

            Assert.Throws<InvalidOperationException>(() => employee.SetSupervisor(employee));
        }
        
        [Test]
        public void TestRemoveSupervisor()
        {
            var supervisor = new Employee("Manager", "Manager", 60000f);
            var subordinate = new Employee("Employee", "Sales Associate", 35000f);

            subordinate.SetSupervisor(supervisor);
            subordinate.RemoveSupervisor();

            Assert.IsNull(subordinate.Supervisor);
            Assert.IsFalse(supervisor.Subordinates.Contains(subordinate));
        }
        
        [Test]
        public void TestAddShift()
        {
            var employee = new Employee("Employee", "Sales Associate", 35000f);
            var store = new Store(); 
            var shift = new EmployeeShift(DateTime.Now, TimeSpan.FromHours(9), TimeSpan.FromHours(17), employee, store, "Cashier");
            
            employee.AddShift(shift);
            
            Assert.Contains(shift, (System.Collections.ICollection)employee.Shifts); 
            Assert.AreEqual(employee, shift.Employee); 
        }

        [Test]
        public void TestAddShiftThrowsExceptionForNull()
        {
            var employee = new Employee("Employee", "Sales Associate", 35000f);

            Assert.Throws<ArgumentException>(() => employee.AddShift(null));
        }
        
        [Test]
        public void TestRemoveShift()
        {
            var employee = new Employee("Employee", "Sales Associate", 35000f);
            var store = new Store(); 
            var shift = new EmployeeShift(DateTime.Now, TimeSpan.FromHours(9), TimeSpan.FromHours(17), employee, store, "Cashier");

            employee.AddShift(shift);
            employee.RemoveShift(shift);

            Assert.IsFalse(employee.Shifts.Contains(shift));
            Assert.IsNull(shift.Employee);
        }
    }

   public class EmployeeShiftTests
    {
        [Test]
        public void TestConstructor()
        {
            DateTime shiftDate = new DateTime(2024, 11, 10);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            Employee employee = new Employee("John Doe", "Cashier", 3000.0f);
            Store store = new Store("SuperMart","New York street","8AM-10PM");
            string role = "Cashier";

            var employeeShift = new EmployeeShift(shiftDate, startTime, endTime, employee, store, role);

            Assert.AreEqual(shiftDate, employeeShift.ShiftDate);
            Assert.AreEqual(startTime, employeeShift.StartTime);
            Assert.AreEqual(endTime, employeeShift.EndTime);
            Assert.AreEqual(1, employeeShift.ShiftId);
            Assert.AreEqual(employee, employeeShift.Employee);
            Assert.AreEqual(store, employeeShift.Store);
            Assert.AreEqual(role, employeeShift.Role);
        }

        [Test]
        public void TestSetShiftDateThrowException()
        {
            var employeeShift = new EmployeeShift(new DateTime(2024, 11, 10), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 
                                                    new Employee("John Doe", "Cashier", 3000.0f), new Store("SuperMart","New York street","8AM-10PM"), "Cashier");

            Assert.Throws<ArgumentException>(() => employeeShift.ShiftDate = default);
        }

        [Test]
        public void TestSetStartTimeThrowException()
        {
            var employeeShift = new EmployeeShift(new DateTime(2024, 11, 10), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 
                new Employee("John Doe", "Cashier", 3000.0f), new Store("SuperMart","New York street","8AM-10PM"), "Cashier");

            Assert.Throws<ArgumentException>(() => employeeShift.StartTime = default);
        }

        [Test]
        public void TestSetEndTimeThrowException()
        {
            var employeeShift = new EmployeeShift(new DateTime(2024, 11, 10), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 
                new Employee("John Doe", "Cashier", 3000.0f), new Store("SuperMart","New York street","8AM-10PM"), "Cashier");

            Assert.Throws<ArgumentException>(() => employeeShift.EndTime = default);
            Assert.Throws<ArgumentException>(() => employeeShift.EndTime = new TimeSpan(8, 0, 0));
        }

        [Test]
        public void TestGetShiftDate()
        {
            var employeeShift = new EmployeeShift(new DateTime(2024, 11, 10), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 
                new Employee("John Doe", "Cashier", 3000.0f), new Store("SuperMart","New York street","8AM-10PM"), "Cashier");

            Assert.AreEqual(new DateTime(2024, 11, 10), employeeShift.ShiftDate);
        }

        [Test]
        public void TestGetStartTime()
        {
            var employeeShift = new EmployeeShift(new DateTime(2024, 11, 10), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 
                new Employee("John Doe", "Cashier", 3000.0f), new Store("SuperMart","New York street","8AM-10PM"), "Cashier");

            Assert.AreEqual(new TimeSpan(9, 0, 0), employeeShift.StartTime);
        }

        [Test]
        public void TestGetEndTime()
        {
            var employeeShift = new EmployeeShift(new DateTime(2024, 11, 10), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 
                new Employee("John Doe", "Cashier", 3000.0f), new Store("SuperMart","New York street","8AM-10PM"), "Cashier");

            Assert.AreEqual(new TimeSpan(17, 0, 0), employeeShift.EndTime);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = EmployeeShift.GetExtent().Count;
            var employeeShift = new EmployeeShift(new DateTime(2024, 11, 10), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 
                new Employee("John Doe", "Cashier", 3000.0f), new Store("SuperMart","New York street","8AM-10PM"), "Cashier");

            var newCount = EmployeeShift.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_employeeshifts.xml";
            var employeeShift = new EmployeeShift(new DateTime(2024, 11, 10), new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 
                new Employee("John Doe", "Cashier", 3000.0f), new Store("SuperMart","New York street","8AM-10PM"), "Cashier");

            EmployeeShift.SaveExtent(path);

            EmployeeShift.LoadExtent(path);
            var loadedExtent = EmployeeShift.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(employeeShift.ShiftDate, loadedExtent[^1].ShiftDate);
            Assert.AreEqual(employeeShift.StartTime, loadedExtent[^1].StartTime);
            Assert.AreEqual(employeeShift.EndTime, loadedExtent[^1].EndTime);

            File.Delete(path);
        }

        [Test]
        public void TestLoadExtentWhenFileDoesNotExist()
        {
            string path = "non_existent_file.xml";
            
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            var result = EmployeeShift.LoadExtent(path);
            
            Assert.IsFalse(result, "LoadExtent should return false when the file does not exist.");

        }

    }

    public class ManagerTest
{
    [Test]
    public void TestConstructor()
    {
        var employee = new Employee("John Doe", "Manager", 50000.0f);
        string storeArea = "Electronics";

        var manager = new Manager(employee, storeArea);

        Assert.AreEqual(employee.Name, manager.Name);
        Assert.AreEqual(employee.Role, manager.Role);
        Assert.AreEqual(employee.Salary, manager.Salary);
        Assert.AreEqual(storeArea, manager.StoreArea);
    }

    [Test]
    public void TestSetStoreAreaThrowException()
    {
        var employee = new Employee("John Doe", "Manager", 50000.0f);
        var manager = new Manager(employee, "Electronics");

        Assert.Throws<ArgumentException>(() => manager.StoreArea = string.Empty);
        Assert.Throws<ArgumentException>(() => manager.StoreArea = null);
    }

    [Test]
    public void TestGetStoreArea()
    {
        var employee = new Employee("John Doe", "Manager", 50000.0f);
        var manager = new Manager(employee, "Electronics");

        Assert.AreEqual("Electronics", manager.StoreArea);
    }

    [Test]
    public void TestExtentIncreasing()
    {
        var initialCount = Manager.GetExtent().Count;
        var employee = new Employee("Jane Smith", "Manager", 60000.0f);
        var manager = new Manager(employee, "Clothing");

        var newCount = Manager.GetExtent().Count;

        Assert.AreEqual(initialCount + 1, newCount);
    }

    [Test]
    public void TestSaveAndLoadExtent()
    {
        string path = "test_managers.xml";
        var employee = new Employee("John Doe", "Manager", 50000.0f);
        var manager = new Manager(employee, "Electronics");
        Manager.SaveExtent(path);

        Manager.LoadExtent(path);
        var loadedExtent = Manager.GetExtent();

        Assert.IsNotEmpty(loadedExtent);
        Assert.IsTrue(File.Exists(path));
        Assert.AreEqual(manager.StoreArea, loadedExtent[^1].StoreArea);

        File.Delete(path);
    }

    [Test]
    public void TestLoadExtentWhenFileDoesNotExist()
    {
        string path = "non_existent_file.xml";

        var result = Manager.LoadExtent(path);

        Assert.IsFalse(result);
        Assert.AreEqual(0, Manager.GetExtent().Count);
    }

    [Test]
    public void TestSaveExtentToFile()
    {
        string path = "managers.xml";
        var employee = new Employee("Alice Brown", "Manager", 55000.0f);
        var manager = new Manager(employee, "Home Appliances");
        Manager.SaveExtent(path);

        Assert.IsTrue(File.Exists(path));
        File.Delete(path);
    }
    
    [Test]
    public void TestSwitchToManagerExtent()
    {
        Manager.ClearExtent();
        var initialCount = Manager.GetExtent().Count;
        var employee = new Employee("Eve White", "Manager", 70000.0f);
        var manager = new Manager(employee, "Furniture");
        
        Manager.SwitchToManagerExtent(manager);
        
        var newCount = Manager.GetExtent().Count;
        Assert.AreEqual(initialCount + 2, newCount);
        Assert.IsTrue(Manager.GetExtent().Contains(manager));
    }

}

    public class OrderTest
    {
        [SetUp]
        public void Setup()
        {
            Order.ClearExtent();
        }

        [Test]
        public void TestConstructor()
        {
            DateTime date = DateTime.Now;

            var order = new Order(date);

            Assert.AreEqual(date, order.Date);
            Assert.AreEqual(1, order.OrderId);
        }

        [Test]
        public void TestSetDate()
        {
            var order = new Order(DateTime.Now);
            DateTime newDate = DateTime.Now.AddDays(1);

            order.Date = newDate;

            Assert.AreEqual(newDate, order.Date);
        }

        [Test]
        public void TestCalculateTotalAmount()
        {
            var order = new Order(DateTime.Now);
            Assert.AreEqual(0, order.TotalAmount);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = Order.GetExtent().Count;
            var order = new Order(DateTime.Now);

            var newCount = Order.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_orders.xml";
            var order = new Order(DateTime.Now);
            Order.SaveExtent(path);

            Order.LoadExtent(path);
            var loadedExtent = Order.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));

            File.Delete(path);
        }

        [Test]
        public void TestLoadExtentWhenFileDoesNotExist()
        {
            string path = "non_existent_file.xml";

            var result = Order.LoadExtent(path);

            Assert.IsFalse(result);
            Assert.AreEqual(0, Order.GetExtent().Count);
        }

        [Test]
        public void TestSaveExtentToFile()
        {
            string path = "orders.xml";
            var order = new Order(DateTime.Now);
            Order.SaveExtent(path);

            Assert.IsTrue(File.Exists(path));
            File.Delete(path);
        }

        [Test]
        public void TestSetCustomer()
        {
            var order = new Order(DateTime.Now);
            var address = new Address("123 Main St", "Cityville", "12345");
            var customer = new Customer(
                name: "John Doe",
                email: "johndoe@example.com",
                phoneNumber: "123-456-7890",
                address: address
            );

            order.SetCustomer(customer);

            Assert.AreEqual(customer, order.Customer);
            Assert.IsTrue(customer.Orders.Contains(order));
        }


        [Test]
        public void TestRemoveCustomer()
        {
            var order = new Order(DateTime.Now);
            var address = new Address("123 Main St", "Cityville", "12345");
            var customer = new Customer(
                name: "John Doe",
                email: "johndoe@example.com",
                phoneNumber: "123-456-7890",
                address: address
            );

            order.SetCustomer(customer);
            order.RemoveCustomer();

            Assert.IsNull(order.Customer);
            Assert.IsFalse(customer.Orders.Contains(order));
        }


        [Test]
        public void TestSetPayment()
        {
            var order = new Order(DateTime.Now);
            var payment = new Payment(100.0f, "CreditCard"); 

            order.SetPayment(payment);

            Assert.AreEqual(payment, order.Payment);
            Assert.AreEqual(order, payment.Order);
        }

        [Test]
        public void TestRemovePayment()
        {
            var order = new Order(DateTime.Now);
            var payment = new Payment(100.0f, "CreditCard"); 

            order.SetPayment(payment);
            order.RemovePayment();

            Assert.IsNull(order.Payment);
            Assert.IsNull(payment.Order);
        }


        [Test]
        public void TestAddTransaction()
        {
            var order = new Order(DateTime.Now);
            var transaction = new Transaction(DateTime.Now, 50.0f); 

            order.AddTransaction(transaction);
            
            Assert.That(order.Transactions.ToList(), Contains.Item(transaction));
            
            Assert.AreEqual(order, transaction.Orders.FirstOrDefault());
        }


        [Test]
        public void TestRemoveTransaction()
        {
            var order = new Order(DateTime.Now);
            var transaction = new Transaction(DateTime.Now, 50.0f); 

            order.AddTransaction(transaction);
            order.RemoveTransaction(transaction);
            
            Assert.That(order.Transactions.ToList(), Does.Not.Contain(transaction));
            
            Assert.IsNull(transaction.Orders.FirstOrDefault());
        }



        [Test]
        public void TestUpdateTransaction()
        {
            var order = new Order(DateTime.Now);
            var oldTransaction = new Transaction(DateTime.Now, 50.0f);  
            var newTransaction = new Transaction(DateTime.Now, 100.0f);  

            order.AddTransaction(oldTransaction);
            order.UpdateTransaction(oldTransaction, newTransaction);

            Assert.IsFalse(order.Transactions.Contains(oldTransaction));
            Assert.Contains(newTransaction, order.Transactions.ToList());
            Assert.IsTrue(order.Transactions.Contains(newTransaction)); 

        }

        [Test]
        public void TestAddDiscount()
        {
            var order = new Order(DateTime.Now);
            var discount = new Discount("Promo", 10.0f, DateTime.Now);

            order.AddDiscount(discount);

            Assert.Contains(discount, order.Discounts.ToList());
            Assert.AreEqual(order, discount.Order);
        }

        [Test]
        public void TestRemoveDiscount()
        {
            var order = new Order(DateTime.Now);
            var discount = new Discount("Promo", 10.0f, DateTime.Now);

            order.AddDiscount(discount);
            order.RemoveDiscount(discount);

            Assert.IsFalse(order.Discounts.Contains(discount));
            Assert.IsNull(discount.Order);
        }

        [Test]
        public void TestAddProduct()
        {
            var order = new Order(DateTime.Now);
            var product = new Product("Laptop", 1500.0f, 1);

            order.AddProduct(product);

            Assert.Contains(product, order.Products.ToList());
            Assert.Contains(order, product.Orders.ToList());
        }

        [Test]
        public void TestRemoveProduct()
        {
            var order = new Order(DateTime.Now);
            var product = new Product("Laptop", 1500.0f,1);

            order.AddProduct(product);
            order.RemoveProduct(product);

            Assert.IsFalse(order.Products.Contains(product));
            Assert.IsFalse(product.Orders.Contains(order));
        }
    }

    public class PaymentTest
    {
        [SetUp]
        public void Setup()
        {
            Payment.ClearExtent();
        }
        
        [Test]
        public void TestConstructor()
        {
            float amount = 100.50f;
            string paymentMethod = "Credit Card";

            var payment = new Payment(amount, paymentMethod);

            Assert.AreEqual(amount, payment.Amount);
            Assert.AreEqual(paymentMethod, payment.PaymentMethod);
            Assert.AreEqual(1, payment.PaymentId);
        }

        [Test]
        public void TestSetAmountThrowException()
        {
            var payment = new Payment(100.50f, "Credit Card");

            Assert.Throws<ArgumentException>(() => payment.Amount = -10f);
            Assert.Throws<ArgumentException>(() => payment.Amount = 0);
        }

        [Test]
        public void TestSetPaymentMethodThrowException()
        {
            var payment = new Payment(100.50f, "Credit Card");

            Assert.Throws<ArgumentException>(() => payment.PaymentMethod = null);
            Assert.Throws<ArgumentException>(() => payment.PaymentMethod = "");
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = Payment.GetExtent().Count;
            var payment = new Payment(50.75f, "Cash");

            var newCount = Payment.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_payments.xml";
            var payment = new Payment(150.25f, "Debit Card");
            Payment.SaveExtent(path);

            Payment.LoadExtent(path);
            var loadedExtent = Payment.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));

            File.Delete(path);
        }

        [Test]
        public void TestLoadExtentWhenFileDoesNotExist()
        {
            string path = "non_existent_file.xml";

            var result = Payment.LoadExtent(path);

            Assert.IsFalse(result);
            Assert.AreEqual(0, Payment.GetExtent().Count);
        }

        [Test]
        public void TestSaveExtentToFile()
        {
            string path = "payments.xml";
            var payment = new Payment(75.00f, "PayPal");
            Payment.SaveExtent(path);

            Assert.IsTrue(File.Exists(path));
            File.Delete(path);
        }
        
        [Test]
        public void TestSetOrder()
        {
            var order = new Order(DateTime.Now);
            var payment = new Payment(100.50f, "Credit Card");

            payment.SetOrder(order);

            Assert.AreEqual(order, payment.Order);
            Assert.AreEqual(payment, order.Payment);
        }
        
        [Test]
        public void TestSetTransaction()
        {
            var transaction = new Transaction(DateTime.Now, 50.0f);
            var payment = new Payment(100.50f, "Credit Card");

            payment.SetTransaction(transaction);

            Assert.AreEqual(transaction, payment.Transaction);
            Assert.AreEqual(payment, transaction.Payment);
        }
        
        [Test]
        public void TestRemoveOrder()
        {
            var order = new Order(DateTime.Now);
            var payment = new Payment(100.50f, "Credit Card");

            payment.SetOrder(order); 
            payment.RemoveOrder(); 

            Assert.IsNull(payment.Order);  
            Assert.IsNull(order.Payment); 
        }
        
        [Test]
        public void TestRemoveTransaction()
        {
            var transaction = new Transaction(DateTime.Now, 50.0f);
            var payment = new Payment(100.50f, "Credit Card");

            payment.SetTransaction(transaction); 
            payment.RemoveTransaction(); 

            Assert.IsNull(payment.Transaction);  
            Assert.IsNull(transaction.Payment); 
        }
    }

    public class ProductTests
    {
        [SetUp]
        public void Setup()
        {
            Product.ClearExtent();
        }

        [Test]
        public void TestConstructor()
        {
            string name = "Shirt";
            float price = 25.99f;
            int stockQuantity = 10;

            var product = new Product(name, price, stockQuantity);

            Assert.AreEqual(name, product.Name);
            Assert.AreEqual(price, product.Price);
            Assert.AreEqual(stockQuantity, product.StockQuantity);
            Assert.AreEqual(1, product.ProductId);
        }

        [Test]
        public void TestNameValidation()
        {
            var product = new Product("Shirt", 25.99f, 10);

            Assert.Throws<ArgumentException>(() => product.Name = "");
            Assert.Throws<ArgumentException>(() => product.Name = null);
        }

        [Test]
        public void TestPriceValidation()
        {
            var product = new Product("Shirt", 25.99f, 10);

            Assert.Throws<ArgumentException>(() => product.Price = -1f);
            Assert.Throws<ArgumentException>(() => product.Price = 0);
        }

        [Test]
        public void TestStockQuantityValidation()
        {
            var product = new Product("Shirt", 25.99f, 10);

            Assert.Throws<ArgumentException>(() => product.StockQuantity = -5);
        }

        [Test]
        public void TestExtentTracking()
        {
            var initialCount = Product.GetExtent().Count;
            var product = new Product("Shirt", 25.99f, 10);

            var newCount = Product.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_products.xml";
            var product = new Product("Shirt", 25.99f, 10);
            Product.SaveExtent(path);

            Product.LoadExtent(path);
            var loadedExtent = Product.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(product.Name, loadedExtent[0].Name);

            File.Delete(path);
        }

        [Test]
        public void TestLoadExtentWhenFileDoesNotExist()
        {
            string path = "non_existent_file.xml";

            var result = Product.LoadExtent(path);

            Assert.IsFalse(result);
            Assert.AreEqual(0, Product.GetExtent().Count);
        }

        [Test]
        public void TestSaveExtentToFile()
        {
            string path = "products.xml";
            var product = new Product("Shirt", 25.99f, 10);
            Product.SaveExtent(path);

            Assert.IsTrue(File.Exists(path));
            File.Delete(path);
        }

        [Test]
        public void TestSetSupplier()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");
            var product = new Product("Shirt", 25.99f, 10);

            product.SetSupplier(supplier);

            Assert.AreEqual(supplier, product.Supplier);
            Assert.Contains(product, supplier.SuppliedProducts.ToList());
        }

        [Test]
        public void TestRemoveSupplier()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");
            var product = new Product("Shirt", 25.99f, 10);

            product.SetSupplier(supplier);
            product.RemoveSupplier();

            Assert.IsNull(product.Supplier);
            Assert.IsFalse(supplier.SuppliedProducts.Contains(product));
        }

        [Test]
        public void TestAddOrder()
        {
            var product = new Product("Shirt", 25.99f, 10);
            var order = new Order();

            product.AddOrder(order);

            Assert.Contains(order, product.Orders.ToList());
            Assert.Contains(product, order.Products.ToList());
        }

        [Test]
        public void TestRemoveOrder()
        {
            var product = new Product("Shirt", 25.99f, 10);
            var order = new Order();

            product.AddOrder(order);
            product.RemoveOrder(order);

            Assert.IsFalse(product.Orders.Contains(order));
            Assert.IsFalse(order.Products.Contains(product));
        }

        [Test]
        public void TestAddStore()
        {
            var product = new Product("Shirt", 25.99f, 10);
            var store = new Store("Main Street Store", "123 Main Street", "9 AM - 9 PM");

            product.AddStore(store);

            Assert.Contains(store, product.Stores.ToList());
            Assert.Contains(product, store.Products.ToList());
        }

        [Test]
        public void TestRemoveStore()
        {
            var product = new Product("Shirt", 25.99f, 10);
            var store = new Store("Main Street Store", "123 Main Street", "9 AM - 9 PM");


            product.AddStore(store);
            product.RemoveStore(store);

            Assert.IsFalse(product.Stores.Contains(store));
            Assert.IsFalse(store.Products.Contains(product));
        }

        [Test]
        public void TestUpdateStore()
        {
            var product = new Product("Shirt", 25.99f, 10);
            var oldStore = new Store("Main Street Store", "123 Main Street", "9 AM - 9 PM");
            var newStore = new Store("Main Street Store", "999 Main Street", "9 AM - 9 PM");

            product.AddStore(oldStore);
            product.UpdateStore(oldStore, newStore);

            Assert.IsFalse(product.Stores.Contains(oldStore));
            Assert.Contains(newStore, product.Stores.ToList());
        }
    }

    public class SalesAssociateTest
    {
        [Test]
        public void TestConstructor()
        {
            var employee = new Employee("John Doe", "SalesAssociate", 3000f);
            float commission = 500f;

            var salesAssociate = new SalesAssociate(employee, commission);

            Assert.AreEqual(employee.Name, salesAssociate.Name);
            Assert.AreEqual(employee.Role, salesAssociate.Role);
            Assert.AreEqual(employee.Salary, salesAssociate.Salary);
            Assert.AreEqual(commission, salesAssociate.SalesCommission);
        }

        [Test]
        public void TestSetSalesCommissionThrowException()
        {
            var employee = new Employee("John Doe", "Sales Associate", 3000f);
            var salesAssociate = new SalesAssociate(employee, 500f);

            Assert.Throws<ArgumentException>(() => salesAssociate.SalesCommission = -100f);
        }

        [Test]
        public void TestGetSalesCommission()
        {
            var employee = new Employee("John Doe", "Sales Associate", 3000f);
            var salesAssociate = new SalesAssociate(employee, 500f);
            
            Assert.AreEqual(500f, salesAssociate.SalesCommission);
        }


        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = SalesAssociate.GetExtent().Count;
            var employee = new Employee("Jane Doe", "Sales Associate", 3500f);
            var salesAssociate = new SalesAssociate(employee, 600f);

            var newCount = SalesAssociate.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_salesassociates.xml";

            var employee = new Employee("John Smith", "Sales Associate", 4000f);
            var salesAssociate = new SalesAssociate(employee, 700f);

            SalesAssociate.SaveExtent(path);

            SalesAssociate.LoadExtent(path);
            var loadedExtent = SalesAssociate.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(salesAssociate.Name, loadedExtent[^1].Name);
            Assert.AreEqual(salesAssociate.Role, loadedExtent[^1].Role);
            Assert.AreEqual(salesAssociate.Salary, loadedExtent[^1].Salary);
            Assert.AreEqual(salesAssociate.SalesCommission, loadedExtent[^1].SalesCommission);
            
            File.Delete(path);
        }


        [Test]
        public void TestLoadExtent_Failure()
        {
            string path = "nonExistentFile.xml";

            bool loaded = SalesAssociate.LoadExtent(path);

            Assert.IsFalse(loaded);
        }
        
        [Test]
        public void TestSwitchToSalesAssociateExtent()
        {
            var initialCount = SalesAssociate.GetExtent().Count;

            var employee = new Employee("Mark Smith", "Sales Associate", 3500f);
            var salesAssociate = new SalesAssociate(employee, 600f);
            
            SalesAssociate.SwitchToSalesAssociateExtent(salesAssociate);

            var newCount = SalesAssociate.GetExtent().Count;
            
            Assert.AreEqual(initialCount + 2, newCount);
            
            Assert.IsTrue(SalesAssociate.GetExtent().Contains(salesAssociate));
        }

    }

public class StoreTest
{
    [Test]
    public void TestConstructor()
    {
        string storeName = "SuperMart";
        string storeLocation = "Downtown";
        string storeHours = "9 AM - 9 PM";

        var store = new Store(storeName, storeLocation, storeHours);

        Assert.AreEqual(storeName, store.StoreName);
        Assert.AreEqual(storeLocation, store.StoreLocation);
        Assert.AreEqual(storeHours, store.StoreHours);
    }

    [Test]
    public void TestSetStoreNameThrowException()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");

        Assert.Throws<ArgumentException>(() => store.StoreName = null);
        Assert.Throws<ArgumentException>(() => store.StoreName = "");
    }

    [Test]
    public void TestSetStoreLocationThrowException()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");

        Assert.Throws<ArgumentException>(() => store.StoreLocation = null);
        Assert.Throws<ArgumentException>(() => store.StoreLocation = "");
    }

    [Test]
    public void TestSetStoreHoursThrowException()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");

        Assert.Throws<ArgumentException>(() => store.StoreHours = null);
        Assert.Throws<ArgumentException>(() => store.StoreHours = "");
    }

    [Test]
    public void TestGetStoreName()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");

        Assert.AreEqual("SuperMart", store.StoreName);
    }

    [Test]
    public void TestGetStoreLocation()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");

        Assert.AreEqual("Downtown", store.StoreLocation);
    }

    [Test]
    public void TestGetStoreHours()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");

        Assert.AreEqual("9 AM - 9 PM", store.StoreHours);
    }

    [Test]
    public void TestExtentIncreasing()
    {
        var initialCount = Store.GetExtent().Count;
        var store = new Store("MegaStore", "Uptown", "10 AM - 8 PM");

        var newCount = Store.GetExtent().Count;

        Assert.AreEqual(initialCount + 1, newCount);
    }

    [Test]
    public void TestSaveAndLoadExtent()
    {
        string path = "test_stores.xml";
        var store = new Store("SuperStore", "Suburbs", "10 AM - 10 PM");
        Store.SaveExtent(path);

        Store.LoadExtent(path);
        var loadedExtent = Store.GetExtent();

        Assert.IsNotEmpty(loadedExtent);
        Assert.IsTrue(File.Exists(path));
        Assert.AreEqual(store.StoreName, loadedExtent[^1].StoreName);
        Assert.AreEqual(store.StoreLocation, loadedExtent[^1].StoreLocation);
        Assert.AreEqual(store.StoreHours, loadedExtent[^1].StoreHours);

        File.Delete(path);
    }

    [Test]
    public void TestLoadExtent_Failure()
    {
        string path = "nonExistentFile.xml";

        bool loaded = Store.LoadExtent(path);

        Assert.IsFalse(loaded);
    }
    
    [Test]
    public void TestAddProductToStore()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");
        var product = new Product("Laptop", 1500.0f, 1);

        store.AddProduct(product);

        Assert.IsTrue(store.Products.Contains(product));
    }

    [Test]
    public void TestRemoveProductFromStore()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");
        var product = new Product("Laptop", 1500.0f, 1);
        store.AddProduct(product);

        store.RemoveProduct(product);

        Assert.IsFalse(store.Products.Contains(product));
    }

    [Test]
    public void TestAddShiftToStore()
    {
        var employee = new Employee("Employee", "Sales Associate", 35000f);
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");
        var shift = new EmployeeShift(DateTime.Now, TimeSpan.FromHours(9), TimeSpan.FromHours(17), employee, store, "Cashier");

        store.AddShift(shift);

        Assert.IsTrue(store.Shifts.Contains(shift));
    }

    [Test]
    public void TestRemoveShiftFromStore()
    {
        var employee = new Employee("Employee", "Sales Associate", 35000f);
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");
        var shift = new EmployeeShift(DateTime.Now, TimeSpan.FromHours(9), TimeSpan.FromHours(17), employee, store, "Cashier");
        store.AddShift(shift);

        store.RemoveShift(shift);

        Assert.IsFalse(store.Shifts.Contains(shift));
    }

    [Test]
    public void TestAddTransactionToStore()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");
        var transaction = new Transaction(DateTime.Now, 50.0f);

        store.AddTransaction(transaction);

        Assert.IsTrue(store.Transactions.Contains(transaction));
    }

    [Test]
    public void TestRemoveTransactionFromStore()
    {
        var store = new Store("SuperMart", "Downtown", "9 AM - 9 PM");
        var transaction = new Transaction(DateTime.Now, 50.0f);
        store.AddTransaction(transaction);

        store.RemoveTransaction(transaction);

        Assert.IsFalse(store.Transactions.Contains(transaction));
    }
}


    public class SupplierTest
    {
        [Test]
        public void TestConstructor()
        {
            string companyName = "Tech Supplies Inc.";
            string contactInfo = "1234 Tech Street, 555-1234";

            var supplier = new Supplier(companyName, contactInfo);

            Assert.AreEqual(companyName, supplier.CompanyName);
            Assert.AreEqual(contactInfo, supplier.ContactInfo);
        }

        [Test]
        public void TestSetCompanyNameThrowException()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");

            Assert.Throws<ArgumentException>(() => supplier.CompanyName = null);
            Assert.Throws<ArgumentException>(() => supplier.CompanyName = "");
        }

        [Test]
        public void TestSetContactInfoThrowException()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");

            Assert.Throws<ArgumentException>(() => supplier.ContactInfo = null);
            Assert.Throws<ArgumentException>(() => supplier.ContactInfo = "");
        }

        [Test]
        public void TestGetCompanyName()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");

            Assert.AreEqual("Tech Supplies Inc.", supplier.CompanyName);
        }

        [Test]
        public void TestGetContactInfo()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");

            Assert.AreEqual("1234 Tech Street, 555-1234", supplier.ContactInfo);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = Supplier.GetExtent().Count;
            var supplier = new Supplier("Global Supplies Ltd.", "5678 Global Avenue, 555-5678");

            var newCount = Supplier.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_suppliers.xml";
            var supplier = new Supplier("Super Tech Co.", "91011 Super Street, 555-91011");
            Supplier.SaveExtent(path);

            Supplier.LoadExtent(path);
            var loadedExtent = Supplier.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(supplier.CompanyName, loadedExtent[^1].CompanyName);
            Assert.AreEqual(supplier.ContactInfo, loadedExtent[^1].ContactInfo);

            File.Delete(path);
        }

        [Test]
        public void TestLoadExtent_Failure()
        {
            string path = "nonExistentFile.xml";

            bool loaded = Supplier.LoadExtent(path);

            Assert.IsFalse(loaded);
        }
        [Test]
        public void TestAddProduct()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");
            var product = new Product("Shirt", 25.99f, 10);

            supplier.AddProduct(product);

            Assert.Contains(product, supplier.SuppliedProducts.ToList());
            Assert.AreEqual(supplier, product.Supplier);
        }

        [Test]
        public void TestRemoveProduct()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");
            var product = new Product("Shirt", 25.99f, 10);

            supplier.AddProduct(product);
            supplier.RemoveProduct(product);

            Assert.IsFalse(supplier.SuppliedProducts.Contains(product));
            Assert.IsNull(product.Supplier);
        }

        [Test]
        public void TestUpdateProduct()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");
            var oldProduct = new Product("Shirt", 25.99f, 10);
            var newProduct = new Product("Jacket", 49.99f, 5);

            supplier.AddProduct(oldProduct);
            supplier.UpdateProduct(oldProduct, newProduct);

            Assert.IsFalse(supplier.SuppliedProducts.Contains(oldProduct));
            Assert.IsTrue(supplier.SuppliedProducts.Contains(newProduct));
            Assert.AreEqual(supplier, newProduct.Supplier);
            Assert.IsNull(oldProduct.Supplier);
        }

        [Test]
        public void TestGetSuppliedProducts()
        {
            var supplier = new Supplier("Tech Supplies Inc.", "1234 Tech Street, 555-1234");
            var product1 = new Product("Shirt", 25.99f, 10);
            var product2 = new Product("Jacket", 49.99f, 5);

            supplier.AddProduct(product1);
            supplier.AddProduct(product2);

            var suppliedProducts = supplier.SuppliedProducts;

            Assert.AreEqual(2, suppliedProducts.Count);
            Assert.Contains(product1, suppliedProducts.ToList());
            Assert.Contains(product2, suppliedProducts.ToList());
        }
    }

    public class TransactionTest
    {
        [Test]
        public void TestConstructor()
        {
            DateTime transactionDate = new DateTime(2024, 11, 10);
            float amount = 100.50f;

            var transaction = new Transaction(transactionDate, amount);

            Assert.AreEqual(transactionDate, transaction.TransactionDate);
            Assert.AreEqual(amount, transaction.Amount);
        }

        [Test]
        public void TestSetAmountThrowException()
        {
            var transaction = new Transaction(DateTime.Now, 50.0f);

            Assert.Throws<ArgumentException>(() => transaction.Amount = -1.0f);
        }

        [Test]
        public void TestGetTransactionDate()
        {
            DateTime transactionDate = new DateTime(2024, 11, 10);
            var transaction = new Transaction(transactionDate, 100.0f);

            Assert.AreEqual(transactionDate, transaction.TransactionDate);
        }

        [Test]
        public void TestGetAmount()
        {
            var transaction = new Transaction(DateTime.Now, 50.0f);

            Assert.AreEqual(50.0f, transaction.Amount);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = Transaction.GetExtent().Count;
            var transaction = new Transaction(DateTime.Now, 100.0f);

            var newCount = Transaction.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void TestSaveAndLoadExtent()
        {
            string path = "test_transactions.xml";
            var transaction = new Transaction(DateTime.Now, 200.0f);
            Transaction.SaveExtent(path);

            Transaction.LoadExtent(path);
            var loadedExtent = Transaction.GetExtent();

            Assert.IsNotEmpty(loadedExtent);
            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(transaction.TransactionDate, loadedExtent[^1].TransactionDate);
            Assert.AreEqual(transaction.Amount, loadedExtent[^1].Amount);

            File.Delete(path);
        }

        [Test]
        public void TestLoadExtent_Failure()
        {
            string path = "nonExistentFile.xml";

            bool loaded = Transaction.LoadExtent(path);

            Assert.IsFalse(loaded);
        }
        
        [Test]
    public void TestSetPayment()
    {
        var payment = new Payment(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.SetPayment(payment);

        Assert.AreEqual(payment, transaction.Payment);
    }

    [Test]
    public void TestRemovePayment()
    {
        var payment = new Payment(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.SetPayment(payment);
        transaction.RemovePayment();

        Assert.IsNull(transaction.Payment);
    }

    [Test]
    public void TestAddOrder()
    {
        var order = new Order(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.AddOrder(order);

        Assert.Contains(order, (System.Collections.ICollection)transaction.Orders);
    }

    [Test]
    public void TestRemoveOrder()
    {
        var order = new Order(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.AddOrder(order);
        transaction.RemoveOrder(order);

        Assert.IsFalse(transaction.Orders.Contains(order));
    }

    [Test]
    public void TestUpdateOrder()
    {
        var oldOrder = new Order(); 
        var newOrder = new Order(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.AddOrder(oldOrder);
        transaction.UpdateOrder(oldOrder, newOrder);

        Assert.Contains(newOrder, (System.Collections.ICollection)transaction.Orders);
        Assert.IsFalse(transaction.Orders.Contains(oldOrder));
    }

    [Test]
    public void TestSetCustomer()
    {
        var customer = new Customer(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.SetCustomer(customer);

        Assert.AreEqual(customer, transaction.Customer);
    }

    [Test]
    public void TestRemoveCustomer()
    {
        var customer = new Customer(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.SetCustomer(customer);
        transaction.RemoveCustomer();

        Assert.IsNull(transaction.Customer);
    }

    [Test]
    public void TestSetStore()
    {
        var store = new Store(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.SetStore(store);

        Assert.AreEqual(store, transaction.Store);
    }

    [Test]
    public void TestRemoveStore()
    {
        var store = new Store(); 
        var transaction = new Transaction(DateTime.Now, 100.0f);

        transaction.SetStore(store);
        transaction.RemoveStore();

        Assert.IsNull(transaction.Store);
    }
    }

    public class WishlistTest
    {
        [SetUp]
        public void Setup()
        {
            Wishlist.ClearExtent();
        }
        
        [Test]
        public void TestConstructor()
        {
            string name = "My Wishlist";
            var wishlist = new Wishlist(name);

            Assert.AreEqual(name, wishlist.Name);
            Assert.AreEqual(1, wishlist.WishlistId);
            Assert.IsEmpty(wishlist.Items);
        }

        [Test]
        public void TestAddItem()
        {
            var wishlist = new Wishlist("My Wishlist");
            var item = new ClothingItem("T-Shirt", 25.99f, 10, "M", "Cotton", "Red",
                "BrandName");
            wishlist.AddItem(item);

            Assert.Contains(item, wishlist.Items);
        }

        [Test]
        public void TestRemoveItem()
        {
            var wishlist = new Wishlist("My Wishlist");
            var item = new ClothingItem("T-Shirt", 25.99f, 10, "M", "Cotton", "Red",
                "BrandName");
            wishlist.AddItem(item);
            wishlist.RemoveItem(item);

            Assert.IsFalse(wishlist.Items.Contains(item));
        }

        [Test]
        public void TestAddItemThrowsExceptionOnNull()
        {
            var wishlist = new Wishlist("My Wishlist");

            Assert.Throws<ArgumentException>(() => wishlist.AddItem(null));
        }

        [Test]
        public void TestRemoveItemDoesNotThrowExceptionOnItemNotFound()
        {
            var wishlist = new Wishlist("My Wishlist");
            var item = new ClothingItem("T-Shirt", 25.99f, 10, "M", "Cotton", "Red",
                "BrandName"); 
            
            wishlist.RemoveItem(item);

            Assert.IsEmpty(wishlist.Items); 
        }

        [Test]
        public void TestLoadExtent_Failure()
        {
            string path = "nonExistentFile.xml";

            bool loaded = Wishlist.LoadExtent(path);

            Assert.IsFalse(loaded);
        }

        [Test]
        public void TestSetNameThrowsExceptionOnNullOrEmpty()
        {
            var wishlist = new Wishlist("My Wishlist");

            Assert.Throws<ArgumentException>(() => wishlist.Name = "");
            Assert.Throws<ArgumentException>(() => wishlist.Name = null);
        }

        [Test]
        public void TestExtentIncreasing()
        {
            var initialCount = Wishlist.GetExtent().Count;
            var wishlist = new Wishlist("Wishlist 1");

            var newCount = Wishlist.GetExtent().Count;

            Assert.AreEqual(initialCount + 1, newCount);
        }
        
        [Test]
    public void TestSetOwner()
    {
        var wishlist = new Wishlist("My Wishlist");
        var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));

        wishlist.SetOwner(customer);

        Assert.AreEqual(customer, wishlist.Owner);
        Assert.Contains(wishlist, (System.Collections.ICollection)customer.Wishlists);
    }

    [Test]
    public void TestSetOwnerThrowsExceptionWhenOwnerIsNull()
    {
        var wishlist = new Wishlist("My Wishlist");

        Assert.Throws<ArgumentException>(() => wishlist.SetOwner(null));
    }

    [Test]
    public void TestRemoveOwner()
    {
        var wishlist = new Wishlist("My Wishlist");
        var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));

        wishlist.SetOwner(customer);
        wishlist.RemoveOwner();

        Assert.IsNull(wishlist.Owner);
        Assert.IsFalse(customer.Wishlists.Contains(wishlist));
    }

    [Test]
    public void TestRemoveOwnerDoesNotThrowWhenOwnerIsNull()
    {
        var wishlist = new Wishlist("My Wishlist");
        
        wishlist.RemoveOwner();

        Assert.IsNull(wishlist.Owner);
    }

    [Test]
    public void TestAddItemAfterOwnerSet()
    {
        var wishlist = new Wishlist("My Wishlist");
        var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
        wishlist.SetOwner(customer);

        var item = new ClothingItem("T-Shirt", 25.99f, 10, "M", "Cotton", "Red", "BrandName");
        wishlist.AddItem(item);

        Assert.Contains(item, wishlist.Items);
    }

    [Test]
    public void TestRemoveItemAfterOwnerSet()
    {
        var wishlist = new Wishlist("My Wishlist");
        var customer = new Customer("Eve", "eve@example.com", "444555666", new Address("123 Street", "City", "Country"));
        wishlist.SetOwner(customer);

        var item = new ClothingItem("T-Shirt", 25.99f, 10, "M", "Cotton", "Red", "BrandName");
        wishlist.AddItem(item);
        wishlist.RemoveItem(item);

        Assert.IsFalse(wishlist.Items.Contains(item));
    }
    }
}