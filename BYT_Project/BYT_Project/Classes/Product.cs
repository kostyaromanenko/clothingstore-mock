using System.Xml.Serialization;
using BYT_Project.Interfaces;

[Serializable]
[XmlInclude(typeof(AccessoryItem))]
[XmlInclude(typeof(ClothingItem))]
public class Product : IProduct
{
    private static int _nextId = 1;
    private int _productId;

    private string _name;
    private float _price;
    private int _stockQuantity;

    private Supplier _supplier;
    private readonly List<Order> _orders = new List<Order>();
    private readonly List<Store> _stores = new List<Store>();

    private static readonly List<Product> _productExtent = new List<Product>();

    public int ProductId
    {
        get => _productId;
        set => _productId = value;
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

    public float Price
    {
        get => _price;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Price must be positive.");
            _price = value;
        }
    }

    public int StockQuantity
    {
        get => _stockQuantity;
        set
        {
            if (value < 0)
                throw new ArgumentException("StockQuantity cannot be negative.");
            _stockQuantity = value;
        }
    }

    public Supplier Supplier => _supplier;

    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    public IReadOnlyList<Store> Stores => _stores.AsReadOnly();

    public Product()
    {
    }

    public Product(string name, float price, int stockQuantity)
    {
        _productId = _nextId++;
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        AddToExtent(this);
    }
    
    public void DisplayProductDetails()
    {
        Console.WriteLine($"Product Name: {Name}, Price: {Price}");
    }
    
    public void SetSupplier(Supplier supplier)
    {
        if (_supplier != supplier)
        {
            _supplier?.RemoveProduct(this);
            _supplier = supplier;
            supplier?.AddProduct(this);
        }
    }
    
    public void RemoveSupplier()
    {
        if (_supplier != null)
        {
            _supplier.RemoveProduct(this);
            _supplier = null;
        }
    }


    public void AddOrder(Order order)
    {
        if (order == null)
            throw new ArgumentException("Order cannot be null.");

        if (!_orders.Contains(order))
        {
            _orders.Add(order);
            order.AddProduct(this);
        }
    }

    public void RemoveOrder(Order order)
    {
        if (order == null)
            throw new ArgumentException("Order cannot be null.");

        if (_orders.Remove(order))
        {
            order.RemoveProduct(this);
        }
    }

    public void AddStore(Store store)
    {
        if (store == null)
            throw new ArgumentException("Store cannot be null.");

        if (!_stores.Contains(store))
        {
            _stores.Add(store);
            store.AddProduct(this);
        }
    }

    public void RemoveStore(Store store)
    {
        if (store == null)
            throw new ArgumentException("Store cannot be null.");

        if (_stores.Remove(store))
        {
            store.RemoveProduct(this);
        }
    }

    public void UpdateStore(Store oldStore, Store newStore)
    {
        if (oldStore == null || newStore == null)
            throw new ArgumentException("Stores cannot be null.");

        RemoveStore(oldStore);
        AddStore(newStore);
    }

    private static void AddToExtent(Product product)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null.");
        _productExtent.Add(product);
    }

    public static IReadOnlyList<Product> GetExtent() => _productExtent.AsReadOnly();

    public static void SaveExtent(string path = "products.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Product>));
            xmlSerializer.Serialize(file, _productExtent);
        }
    }

    public static bool LoadExtent(string path = "products.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Product>));
                _productExtent.Clear();
                _productExtent.AddRange((List<Product>)xmlSerializer.Deserialize(file));

                if (_productExtent.Count > 0)
                    _nextId = _productExtent[^1].ProductId + 1;
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
        _productExtent.Clear();
        _nextId = 1;
    }
}
