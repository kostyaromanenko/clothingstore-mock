using System.Xml.Serialization;

[Serializable]
public class Discount
{
    private string _discountCode;
    private float _percentage;
    private DateTime _expiryDate;

    private Order? _order;

    private static readonly List<Discount> _discountExtent = new List<Discount>();

    public string DiscountCode
    {
        get => _discountCode;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("DiscountCode cannot be null or empty.");
            _discountCode = value;
        }
    }

    public float Percentage
    {
        get => _percentage;
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Percentage must be between 0 and 100.");
            _percentage = value;
        }
    }

    public DateTime ExpiryDate
    {
        get => _expiryDate;
        set => _expiryDate = value;
    }

    public Order? Order => _order;

    public Discount()
    {
    }

    public Discount(string discountCode, float percentage, DateTime expiryDate)
    {
        DiscountCode = discountCode;
        Percentage = percentage;
        ExpiryDate = expiryDate;
        AddToExtent(this);
    }

    public void SetOrder(Order order)
    {
        if (_order != order)
        {
            _order?.RemoveDiscount(this);
            _order = order;
            order?.AddDiscount(this);
        }
    }

    public void RemoveOrder()
    {
        if (_order != null)
        {
            var tempOrder = _order;
            _order = null;
            tempOrder.RemoveDiscount(this);
        }
    }

    private static void AddToExtent(Discount discount)
    {
        if (discount == null)
            throw new ArgumentException("Discount cannot be null.");
        _discountExtent.Add(discount);
    }

    public static IReadOnlyList<Discount> GetExtent() => _discountExtent.AsReadOnly();

    public static void SaveExtent(string path = "discounts.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Discount>));
            xmlSerializer.Serialize(file, _discountExtent);
        }
    }

    public static bool LoadExtent(string path = "discounts.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Discount>));
                _discountExtent.Clear();
                _discountExtent.AddRange((List<Discount>)xmlSerializer.Deserialize(file));
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
        _discountExtent.Clear();
    }
}