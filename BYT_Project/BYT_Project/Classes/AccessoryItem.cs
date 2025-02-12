using System.Xml.Serialization;

[Serializable]
public class AccessoryItem : Product
{
    private string _size;
    private string _color;
    private string _brand;

    private static readonly List<AccessoryItem> _accessoryItemExtent = new List<AccessoryItem>();

    public string Size
    {
        get => _size;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Size cannot be null or empty.");
            _size = value;
        }
    }

    public string Color
    {
        get => _color;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Color cannot be null or empty.");
            _color = value;
        }
    }

    public string Brand
    {
        get => _brand;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Brand cannot be null or empty.");
            _brand = value;
        }
    }

    public AccessoryItem()
    {
        
    }

    public AccessoryItem(string name, float price, int stockQuantity, string size, string color, string brand)
        : base(name, price, stockQuantity)
    {
        Size = size;
        Color = color;
        Brand = brand;
        AddToExtent(this);
    }

    private static void AddToExtent(AccessoryItem accessoryItem)
    {
        if (accessoryItem == null)
            throw new ArgumentException("AccessoryItem cannot be null.");
        _accessoryItemExtent.Add(accessoryItem);
    }

    public static IReadOnlyList<AccessoryItem> GetExtent() => _accessoryItemExtent.AsReadOnly();

    public static void SaveExtent(string path = "accessoryitems.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<AccessoryItem>));
            xmlSerializer.Serialize(file, _accessoryItemExtent);
        }
    }

    public static bool LoadExtent(string path = "accessoryitems.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<AccessoryItem>));
                _accessoryItemExtent.Clear();
                _accessoryItemExtent.AddRange((List<AccessoryItem>)xmlSerializer.Deserialize(file));
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
        _accessoryItemExtent.Clear();
        
    }

}
