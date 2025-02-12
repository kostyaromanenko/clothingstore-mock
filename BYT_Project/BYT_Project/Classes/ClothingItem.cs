using System.Xml.Serialization;
using BYT_Project.Interfaces;

[Serializable]
public class ClothingItem : Product, IClothing, IClothingFabricOptions
{
    private string _size;
    private string _color;
    private string _fabric;
    private string _brand;
    
    private static readonly List<ClothingItem> _clothingItemExtent = new List<ClothingItem>();

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

    public string Fabric
    {
        get => _fabric;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Fabric cannot be null or empty.");
            _fabric = value;
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
    
    
    private static readonly List<string> AvailableFabrics = new List<string>
    {
        "Cotton",
        "Polyester",
        "Wool",
        "Silk",
        "Linen"
    };

    public ClothingItem()
    {
        
    }

    public ClothingItem(string name, float price, int stockQuantity, string size, string color, string fabric, string brand)
        : base(name, price, stockQuantity)
    {
        Size = size;
        Color = color;
        Fabric = fabric;
        Brand = brand;
        AddToExtent(this);
    }

    public void DisplayClothingAttributes()
    {
        Console.WriteLine($"Size: {Size}, Color: {Color}, Fabric: {Fabric}");
    }
    
    public List<string> GetAvailableFabrics()
    {
        return AvailableFabrics;
    }

    public void DisplayFabricOptions()
    {
        Console.WriteLine("Available Fabrics:");
        foreach (var fabric in AvailableFabrics)
        {
            Console.WriteLine($"- {fabric}");
        }
    }
    
    private static void AddToExtent(ClothingItem clothingItem)
    {
        if (clothingItem == null)
            throw new ArgumentException("ClothingItem cannot be null.");
        _clothingItemExtent.Add(clothingItem);
    }

    public static IReadOnlyList<ClothingItem> GetExtent() => _clothingItemExtent.AsReadOnly();

    public static void SaveExtent(string path = "clothingitems.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ClothingItem>));
            xmlSerializer.Serialize(file, _clothingItemExtent);
        }
    }

    public static bool LoadExtent(string path = "clothingitems.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ClothingItem>));
                _clothingItemExtent.Clear();
                _clothingItemExtent.AddRange((List<ClothingItem>)xmlSerializer.Deserialize(file));
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
        _clothingItemExtent.Clear();
    }

    
}
