using System.Xml.Serialization;

[Serializable]
public class Address
{
    private static int _nextId = 1;
    private int _addressId;

    private string _street;
    private string _city;
    private string _zipcode;

    private static readonly List<Address> _addressExtent = new List<Address>();

    public int AddressId
    {
        get => _addressId;
        set => _addressId = value;
    }

    public string Street
    {
        get => _street;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Street cannot be null or empty.");
            _street = value;
        }
    }

    public string City
    {
        get => _city;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("City cannot be null or empty.");
            _city = value;
        }
    }

    public string Zipcode
    {
        get => _zipcode;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Zipcode cannot be null or empty.");
            _zipcode = value;
        }
    }

    public Address()
    {
        
    }
    public Address(string street, string city, string zipcode)
    {
        _addressId = _nextId++;
        Street = street;
        City = city;
        Zipcode = zipcode;
        AddToExtent(this);
    }

    private static void AddToExtent(Address address)
    {
        if (address == null)
            throw new ArgumentException("Address cannot be null.");
        _addressExtent.Add(address);
    }

    public static IReadOnlyList<Address> GetExtent() => _addressExtent.AsReadOnly();

    public static void SaveExtent(string path = "addresses.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Address>));
            xmlSerializer.Serialize(file, _addressExtent);
        }
    }

    public static bool LoadExtent(string path = "addresses.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Address>));
                _addressExtent.Clear();
                _addressExtent.AddRange((List<Address>)xmlSerializer.Deserialize(file));

                if (_addressExtent.Count > 0)
                    _nextId = _addressExtent[^1].AddressId + 1;
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
        _addressExtent.Clear();
        _nextId = 1;
    }
    
    
    private Customer _customer;

    public Customer Customer => _customer;

    public void SetCustomer(Customer customer)
    {
        if (_customer == customer) return;

        if (_customer != null && customer != null)
            throw new InvalidOperationException("Address is already associated with a Customer.");

        _customer = customer;

        if (customer != null)
        {
            customer.UpdateAddress(this);
        }
    }
    
}
