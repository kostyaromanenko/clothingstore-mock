
using System.Xml.Serialization;

[Serializable]
public class Wishlist
{
    private static int _nextId = 1;
    private int _wishlistId;
    private string _name;
    private DateTime _createdDate;
    private List<ClothingItem> _items = new List<ClothingItem>();

    private Customer? _owner;
    private static readonly List<Wishlist> _wishlistExtent = new List<Wishlist>();

    public int WishlistId
    {
        get => _wishlistId;
        set => _wishlistId = value;
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

    public DateTime CreatedDate
    {
        get => _createdDate;
        set => _createdDate = value;
    }

    public List<ClothingItem> Items => new List<ClothingItem>(_items);

    public Wishlist()
    {
        _createdDate = DateTime.Now;
    }

    public Wishlist(string name)
    {
        _wishlistId = _nextId++;
        Name = name;
        _createdDate = DateTime.Now;
        AddToExtent(this);
    }

    public void AddItem(ClothingItem item)
    {
        if (item == null)
            throw new ArgumentException("Item cannot be null.");
        _items.Add(item);
    }

    public void RemoveItem(ClothingItem item)
    {
        _items.Remove(item);
    }

    private static void AddToExtent(Wishlist wishlist)
    {
        if (wishlist == null)
            throw new ArgumentException("Wishlist cannot be null.");
        _wishlistExtent.Add(wishlist);
    }

    public static IReadOnlyList<Wishlist> GetExtent() => _wishlistExtent.AsReadOnly();

    public static void SaveExtent(string path = "wishlists.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Wishlist>));
            xmlSerializer.Serialize(file, _wishlistExtent);
        }
    }

    public static bool LoadExtent(string path = "wishlists.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Wishlist>));
                _wishlistExtent.Clear();
                _wishlistExtent.AddRange((List<Wishlist>)xmlSerializer.Deserialize(file));

                if (_wishlistExtent.Count > 0)
                    _nextId = _wishlistExtent[^1].WishlistId + 1;
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
        _wishlistExtent.Clear();
        _nextId = 1;
    }
    
    public Customer? Owner => _owner;
    
    public void SetOwner(Customer owner)
    {
        if (owner == null)
        {
            throw new ArgumentException("Owner cannot be null.");
        }

        if (_owner != owner)
        {
            _owner?.RemoveWishlist(this);
            _owner = owner;
            owner.AddWishlist(this);
        }
    }


    public void RemoveOwner()
    {
        if (_owner != null)
        {
            var previousOwner = _owner;
            _owner = null;
            
            previousOwner.RemoveWishlist(this);
        }
    }


    
}
