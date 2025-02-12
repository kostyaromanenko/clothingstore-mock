using System.Xml.Serialization;

[Serializable]
public class Supplier
{
    private static int _nextId = 1;
    private int _supplierId;

    private string _companyName;
    private string _contactInfo;

    private readonly List<Product> _suppliedProducts = new List<Product>();

    private static readonly List<Supplier> _supplierExtent = new List<Supplier>();

    public int SupplierId
    {
        get => _supplierId;
        set => _supplierId = value;
    }

    public string CompanyName
    {
        get => _companyName;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("CompanyName cannot be null or empty.");
            _companyName = value;
        }
    }

    public string ContactInfo
    {
        get => _contactInfo;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("ContactInfo cannot be null or empty.");
            _contactInfo = value;
        }
    }

    public IReadOnlyList<Product> SuppliedProducts => _suppliedProducts.AsReadOnly();

    public Supplier()
    {
    }

    public Supplier(string companyName, string contactInfo)
    {
        _supplierId = _nextId++;
        CompanyName = companyName;
        ContactInfo = contactInfo;
        AddToExtent(this);
    }

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null.");

        if (!_suppliedProducts.Contains(product))
        {
            _suppliedProducts.Add(product);
            product.SetSupplier(this);
        }
    }

    public void RemoveProduct(Product product)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null.");

        if (_suppliedProducts.Remove(product))
        {
            product.RemoveSupplier();
        }
    }

    public void UpdateProduct(Product oldProduct, Product newProduct)
    {
        if (oldProduct == null || newProduct == null)
            throw new ArgumentException("Products cannot be null.");

        RemoveProduct(oldProduct);
        AddProduct(newProduct);
    }

    private static void AddToExtent(Supplier supplier)
    {
        if (supplier == null)
            throw new ArgumentException("Supplier cannot be null.");
        _supplierExtent.Add(supplier);
    }

    public static IReadOnlyList<Supplier> GetExtent() => _supplierExtent.AsReadOnly();

    public static void SaveExtent(string path = "suppliers.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Supplier>));
            xmlSerializer.Serialize(file, _supplierExtent);
        }
    }

    public static bool LoadExtent(string path = "suppliers.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Supplier>));
                _supplierExtent.Clear();
                _supplierExtent.AddRange((List<Supplier>)xmlSerializer.Deserialize(file));

                if (_supplierExtent.Count > 0)
                    _nextId = _supplierExtent[^1].SupplierId + 1;
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
        _supplierExtent.Clear();
        _nextId = 1;
    }
}