using System.Xml.Serialization;

[Serializable]
public class Customer
{
    private static int _nextId = 1;
    private int _customerId;
    private string _name;
    private string? _middleName;
    private string _email;
    private string _phoneNumber;

    private readonly List<Wishlist> _wishlists = new List<Wishlist>();
    private readonly List<Order> _orders = new List<Order>();
    private readonly List<Transaction> _transactions = new List<Transaction>();

    private static readonly List<Customer> _customerExtent = new List<Customer>();

    public int CustomerId
    {
        get => _customerId;
        set => _customerId = value;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Name cannot be null or empty.");
            _name = value;
        }
    }

    public string? MiddleName
    {
        get => _middleName;
        set => _middleName = value;
    }

    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Email cannot be null or empty.");
            _email = value;
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("PhoneNumber cannot be null or empty.");
            _phoneNumber = value;
        }
    }

    public IReadOnlyList<Wishlist> Wishlists => _wishlists.AsReadOnly();
    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

    public Customer()
    {
    }

    public Customer(string name, string email, string phoneNumber, Address address, string? middleName = null)
    {
        if (address == null)
            throw new ArgumentException("Address cannot be null.");

        _customerId = _nextId++;
        Name = name;
        MiddleName = middleName;
        Email = email;
        PhoneNumber = phoneNumber;
        _address = address;
        address.SetCustomer(this);
        AddToExtent(this);
    }

    private static void AddToExtent(Customer customer)
    {
        if (customer == null)
            throw new ArgumentException("Customer cannot be null.");
        _customerExtent.Add(customer);
    }

    public static IReadOnlyList<Customer> GetExtent() => _customerExtent.AsReadOnly();

    public static void SaveExtent(string path = "customers.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Customer>));
            xmlSerializer.Serialize(file, _customerExtent);
        }
    }

    public static bool LoadExtent(string path = "customers.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Customer>));
                _customerExtent.Clear();
                _customerExtent.AddRange((List<Customer>)xmlSerializer.Deserialize(file));

                if (_customerExtent.Count > 0)
                    _nextId = _customerExtent[^1].CustomerId + 1;
            }
            return true;
        }
        catch (Exception)
        {
            ClearExtent();
            return false;
        }
    }

    public static void ClearExtent()
    {
        _customerExtent.Clear();
        _nextId = 1;
    }

    public void AddOrder(Order order)
    {
        if (order == null)
            throw new ArgumentException("Order cannot be null.");

        if (!_orders.Contains(order))
        {
            _orders.Add(order);
            order.SetCustomer(this);
        }
    }

    public void RemoveOrder(Order order)
    {
        if (order == null)
            throw new ArgumentException("Order cannot be null.");

        if (_orders.Remove(order))
        {
            order.RemoveCustomer();
        }
    }

    public void UpdateOrder(Order oldOrder, Order newOrder)
    {
        if (oldOrder == null || newOrder == null)
            throw new ArgumentException("Orders cannot be null.");

        RemoveOrder(oldOrder);
        AddOrder(newOrder);
    }

    public void AddWishlist(Wishlist wishlist)
    {
        if (wishlist == null)
            throw new ArgumentException("Wishlist cannot be null.");

        if (!_wishlists.Contains(wishlist))
        {
            _wishlists.Add(wishlist);
            wishlist.SetOwner(this);
        }
    }

    public void RemoveWishlist(Wishlist wishlist)
    {
        if (wishlist == null)
            throw new ArgumentException("Wishlist cannot be null.");

        if (_wishlists.Remove(wishlist))
        {
            wishlist.RemoveOwner();
        }
    }

    public void UpdateWishlist(Wishlist oldWishlist, Wishlist newWishlist)
    {
        if (oldWishlist == null || newWishlist == null)
            throw new ArgumentException("Wishlists cannot be null.");

        RemoveWishlist(oldWishlist);
        AddWishlist(newWishlist);
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentException("Transaction cannot be null.");

        if (!_transactions.Contains(transaction))
        {
            _transactions.Add(transaction);
            transaction.SetCustomer(this);
        }
    }

    public void RemoveTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentException("Transaction cannot be null.");

        if (_transactions.Remove(transaction))
        {
            transaction.RemoveCustomer();
        }
    }

    public void UpdateTransaction(Transaction oldTransaction, Transaction newTransaction)
    {
        if (oldTransaction == null || newTransaction == null)
            throw new ArgumentException("Transactions cannot be null.");

        RemoveTransaction(oldTransaction);
        AddTransaction(newTransaction);
    }

    private Address _address;

    public Address Address => _address;

    public void UpdateAddress(Address newAddress)
    {
        if (newAddress == null)
            throw new ArgumentException("Address cannot be null.");
        
        _address = newAddress;
        newAddress.SetCustomer(this);
    }
    public void RemoveAddress()
    {
        if (_address != null)
        {
            _address.SetCustomer(null);
            _address = null;
        }
    }
}