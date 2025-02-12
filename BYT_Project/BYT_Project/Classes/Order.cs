using System.Xml.Serialization;

[Serializable]
public class Order
{
    private static int _nextId = 1;
    private int _orderId;
    private DateTime _date;
    private float _totalAmount;

    private Customer _customer;
    private Payment _payment;
    private readonly List<Transaction> _transactions = new List<Transaction>();
    private readonly List<Discount> _discounts = new List<Discount>();
    private readonly List<Product> _products = new List<Product>();

    private static readonly List<Order> _orderExtent = new List<Order>();

    public int OrderId
    {
        get => _orderId;
        set => _orderId = value;
    }

    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    public float TotalAmount => CalculateTotalAmount();

    public Customer Customer => _customer;
    public Payment Payment => _payment;
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public IReadOnlyList<Discount> Discounts => _discounts.AsReadOnly();
    public IReadOnlyList<Product> Products => _products.AsReadOnly();

    public Order()
    {
    }

    public Order(DateTime date)
    {
        _orderId = _nextId++;
        Date = date;
        AddToExtent(this);
    }

    public void SetCustomer(Customer customer)
    {
        if (_customer != customer)
        {
            _customer?.RemoveOrder(this);
            _customer = customer;
            customer?.AddOrder(this);
        }
    }

    public void RemoveCustomer()
    {
        Customer.RemoveOrder(this);
        SetCustomer(null); 
    }

    public void SetPayment(Payment payment)
    {
        if (_payment != payment)
        {
            _payment?.RemoveOrder();
            _payment = payment;
            payment?.SetOrder(this);
        }
    }

    public void RemovePayment()
    {
        Payment.RemoveOrder();
        SetPayment(null);
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentException("Transaction cannot be null.");

        if (!_transactions.Contains(transaction))
        {
            _transactions.Add(transaction);
            transaction.AddOrder(this);
        }
    }

    public void RemoveTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentException("Transaction cannot be null.");

        if (_transactions.Remove(transaction))
        {
            transaction.RemoveOrder(this);
        }
    }

    public void UpdateTransaction(Transaction oldTransaction, Transaction newTransaction)
    {
        if (oldTransaction == null || newTransaction == null)
            throw new ArgumentException("Transactions cannot be null.");

        RemoveTransaction(oldTransaction);
        AddTransaction(newTransaction);
    }

    public void AddDiscount(Discount discount)
    {
        if (discount == null)
            throw new ArgumentException("Discount cannot be null.");

        if (!_discounts.Contains(discount))
        {
            _discounts.Add(discount);
            discount.SetOrder(this);
        }
    }

    public void RemoveDiscount(Discount discount)
    {
        if (discount == null)
            throw new ArgumentException("Discount cannot be null.");

        if (_discounts.Remove(discount))
        {
            discount.RemoveOrder();
        }
    }

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null.");

        if (!_products.Contains(product))
        {
            _products.Add(product);
            product.AddOrder(this);
        }
    }

    public void RemoveProduct(Product product)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null.");

        if (_products.Remove(product))
        {
            product.RemoveOrder(this);
        }
    }

    private float CalculateTotalAmount()
    {
        return _totalAmount;
    }

    private static void AddToExtent(Order order)
    {
        if (order == null)
            throw new ArgumentException("Order cannot be null.");
        _orderExtent.Add(order);
    }

    public static IReadOnlyList<Order> GetExtent() => _orderExtent.AsReadOnly();

    public static void SaveExtent(string path = "orders.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            xmlSerializer.Serialize(file, _orderExtent);
        }
    }

    public static bool LoadExtent(string path = "orders.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
                _orderExtent.Clear();
                _orderExtent.AddRange((List<Order>)xmlSerializer.Deserialize(file));

                if (_orderExtent.Count > 0)
                    _nextId = _orderExtent[^1].OrderId + 1;
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
        _orderExtent.Clear();
        _nextId = 1;
    }
}