using System.Xml.Serialization;

[Serializable]
public class Transaction
{
    private static int _nextId = 1;
    private int _transactionId;
    private DateTime _transactionDate;
    private float _amount;

    private Payment _payment;
    private readonly List<Order> _orders = new List<Order>();
    private Customer _customer;
    private Store _store;

    private static readonly List<Transaction> _transactionExtent = new List<Transaction>();

    public int TransactionId
    {
        get => _transactionId;
        set => _transactionId = value;
    }

    public DateTime TransactionDate
    {
        get => _transactionDate;
        set => _transactionDate = value;
    }

    public float Amount
    {
        get => _amount;
        set
        {
            if (value < 0)
                throw new ArgumentException("Amount cannot be negative.");
            _amount = value;
        }
    }

    public Payment Payment => _payment;
    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();
    public Customer Customer => _customer;
    public Store Store => _store;

    public Transaction()
    {
    }

    public Transaction(DateTime transactionDate, float amount)
    {
        _transactionId = _nextId++;
        TransactionDate = transactionDate;
        Amount = amount;
        AddToExtent(this);
    }

    public void SetPayment(Payment payment)
    {
        if (_payment != payment)
        {
            _payment?.RemoveTransaction();
            _payment = payment;
            payment?.SetTransaction(this);
        }
    }

    public void RemovePayment()
    {
        _payment = null;
    }

    public void AddOrder(Order order)
    {
        if (order == null)
            throw new ArgumentException("Order cannot be null.");

        if (!_orders.Contains(order))
        {
            _orders.Add(order);
            order.AddTransaction(this);
        }
    }

    public void RemoveOrder(Order order)
    {
        if (order == null)
            throw new ArgumentException("Order cannot be null.");

        if (_orders.Remove(order))
        {
            order.RemoveTransaction(this);
        }
    }

    public void UpdateOrder(Order oldOrder, Order newOrder)
    {
        if (oldOrder == null || newOrder == null)
            throw new ArgumentException("Orders cannot be null.");

        RemoveOrder(oldOrder);
        AddOrder(newOrder);
    }

    public void SetCustomer(Customer customer)
    {
        if (_customer != customer)
        {
            _customer?.RemoveTransaction(this);
            _customer = customer;
            customer?.AddTransaction(this);
        }
    }

    public void RemoveCustomer()
    {
        _customer = null;
    }

    public void SetStore(Store store)
    {
        if (_store != store)
        {
            _store?.RemoveTransaction(this);
            _store = store;
            store.AddTransaction(this);
        }
    }

    public void RemoveStore()
    {
        _store = null;
    }

    private static void AddToExtent(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentException("Transaction cannot be null.");
        _transactionExtent.Add(transaction);
    }

    public static IReadOnlyList<Transaction> GetExtent() => _transactionExtent.AsReadOnly();

    public static void SaveExtent(string path = "transactions.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Transaction>));
            xmlSerializer.Serialize(file, _transactionExtent);
        }
    }

    public static bool LoadExtent(string path = "transactions.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Transaction>));
                _transactionExtent.Clear();
                _transactionExtent.AddRange((List<Transaction>)xmlSerializer.Deserialize(file));

                if (_transactionExtent.Count > 0)
                    _nextId = _transactionExtent[^1].TransactionId + 1;
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
        _transactionExtent.Clear();
        _nextId = 1;
    }
}
