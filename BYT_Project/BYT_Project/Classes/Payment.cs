using System.Xml.Serialization;

[Serializable]
public class Payment
{
    private static int _nextId = 1;
    private int _paymentId;

    private float _amount;
    private string _paymentMethod;

    private Order _order;
    private Transaction _transaction;

    private static readonly List<Payment> _paymentExtent = new List<Payment>();

    public int PaymentId
    {
        get => _paymentId;
        set => _paymentId = value;
    }

    public float Amount
    {
        get => _amount;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Amount must be positive.");
            _amount = value;
        }
    }

    public string PaymentMethod
    {
        get => _paymentMethod;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("PaymentMethod cannot be null or empty.");
            _paymentMethod = value;
        }
    }

    public Order Order => _order;
    public Transaction Transaction => _transaction;

    public Payment()
    {
    }

    public Payment(float amount, string paymentMethod)
    {
        _paymentId = _nextId++;
        Amount = amount;
        PaymentMethod = paymentMethod;
        AddToExtent(this);
    }

    public void SetOrder(Order order)
    {
        if (_order != order)
        {
            _order?.RemovePayment();
            _order = order;
            order?.SetPayment(this);
        }
    }

    public void SetTransaction(Transaction transaction)
    {
        if (_transaction != transaction)
        {
            _transaction?.RemovePayment();
            _transaction = transaction;
            transaction?.SetPayment(this);
        }
    }

    public void RemoveOrder()
    {
        if (_order != null)
        {
            var oldOrder = _order;
            _order = null;
            oldOrder.RemovePayment();
        }
    }


    public void RemoveTransaction()
    {
        if (_transaction != null)
        {
            var oldTransaction = _transaction;
            _transaction = null;
            oldTransaction.RemovePayment();
        }
    }


    private static void AddToExtent(Payment payment)
    {
        if (payment == null)
            throw new ArgumentException("Payment cannot be null.");
        _paymentExtent.Add(payment);
    }

    public static IReadOnlyList<Payment> GetExtent() => _paymentExtent.AsReadOnly();

    public static void SaveExtent(string path = "payments.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Payment>));
            xmlSerializer.Serialize(file, _paymentExtent);
        }
    }

    public static bool LoadExtent(string path = "payments.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Payment>));
                _paymentExtent.Clear();
                _paymentExtent.AddRange((List<Payment>)xmlSerializer.Deserialize(file));

                if (_paymentExtent.Count > 0)
                    _nextId = _paymentExtent[^1].PaymentId + 1;
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
        _paymentExtent.Clear();
        _nextId = 1;
    }
}