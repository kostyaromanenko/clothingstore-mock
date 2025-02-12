using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public class Store
{
    private static int _nextId = 1;
    private int _storeId;
    private string _storeName;
    private string _storeLocation;
    private string _storeHours;

    private readonly List<Product> _products = new List<Product>();
    private readonly List<EmployeeShift> _shifts = new List<EmployeeShift>();
    private readonly List<Transaction> _transactions = new List<Transaction>();
    private readonly Dictionary<string, Employee> _employeesByRole = new Dictionary<string, Employee>();

    private static readonly List<Store> _storeExtent = new List<Store>();

    public int StoreId
    {
        get => _storeId;
        set => _storeId = value;
    }

    public string StoreName
    {
        get => _storeName;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("StoreName cannot be null or empty.");
            _storeName = value;
        }
    }

    public string StoreLocation
    {
        get => _storeLocation;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("StoreLocation cannot be null or empty.");
            _storeLocation = value;
        }
    }

    public string StoreHours
    {
        get => _storeHours;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("StoreHours cannot be null or empty.");
            _storeHours = value;
        }
    }

    public IReadOnlyList<Product> Products => _products.AsReadOnly();
    public IReadOnlyList<EmployeeShift> Shifts => _shifts.AsReadOnly();
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public IReadOnlyDictionary<string, Employee> EmployeesByRole => _employeesByRole;


    public Store()
    {
    }

    public Store(string storeName, string storeLocation, string storeHours)
    {
        _storeId = _nextId++;
        StoreName = storeName;
        StoreLocation = storeLocation;
        StoreHours = storeHours;
        AddToExtent(this);
    }
    
    public Employee? GetEmployeeByRole(string role)
    {
        _employeesByRole.TryGetValue(role, out var employee);
        return employee;
    }
    
    public void AddEmployeeByRole(string role, Employee employee)
    {
        if (string.IsNullOrEmpty(role))
            throw new ArgumentException("Role cannot be null or empty.");
        if (employee == null)
            throw new ArgumentException("Employee cannot be null.");

        if (_employeesByRole.ContainsKey(role))
            throw new InvalidOperationException($"An employee is already assigned to the role '{role}'.");

        _employeesByRole[role] = employee;
        employee.AddStoreByRole(this, role);
    }

    public void RemoveEmployeeByRole(string role)
    {
        if (string.IsNullOrEmpty(role))
            throw new ArgumentException("Role cannot be null or empty.");

        if (_employeesByRole.TryGetValue(role, out var employee))
        {
            _employeesByRole.Remove(role);
            employee.RemoveStoreByRole(this, role);
        }
    }
    
    public void UpdateEmployeeRole(string oldRole, string newRole, Employee employee)
    {
        if (string.IsNullOrEmpty(oldRole) || string.IsNullOrEmpty(newRole))
            throw new ArgumentException("Role cannot be null or empty.");
        if (employee == null)
            throw new ArgumentException("Employee cannot be null.");

        RemoveEmployeeByRole(oldRole);
        AddEmployeeByRole(newRole, employee);
    }

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null.");

        if (!_products.Contains(product))
        {
            _products.Add(product);
            product.AddStore(this);
        }
    }

    public void RemoveProduct(Product product)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null.");

        if (_products.Remove(product))
        {
            product.RemoveStore(this);
        }
    }

    public void AddShift(EmployeeShift shift)
    {
        if (shift == null)
            throw new ArgumentException("Shift cannot be null.");
        if (!_shifts.Contains(shift))
        {
            _shifts.Add(shift);
            shift.SetStore(this);
        }
    }
    

    public void RemoveShift(EmployeeShift shift)
    {
        if (shift == null) throw new ArgumentException("Shift cannot be null.");

        if (_shifts.Remove(shift))
        {
            if (shift.Store == this)
            {
                shift.SetStore(null);
            }
        }
    }


    
    
    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentException("Transaction cannot be null.");

        if (!_transactions.Contains(transaction))
        {
            _transactions.Add(transaction);
            transaction.SetStore(this);
        }
    }

    public void RemoveTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentException("Transaction cannot be null.");

        if (_transactions.Remove(transaction))
        {
            transaction.RemoveStore();
        }
    }

    private static void AddToExtent(Store store)
    {
        if (store == null)
            throw new ArgumentException("Store cannot be null.");
        _storeExtent.Add(store);
    }

    public static IReadOnlyList<Store> GetExtent() => _storeExtent.AsReadOnly();

    public static void SaveExtent(string path = "stores.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Store>));
            xmlSerializer.Serialize(file, _storeExtent);
        }
    }

    public static bool LoadExtent(string path = "stores.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Store>));
                _storeExtent.Clear();
                _storeExtent.AddRange((List<Store>)xmlSerializer.Deserialize(file));

                if (_storeExtent.Count > 0)
                    _nextId = _storeExtent[^1].StoreId + 1;
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
        _storeExtent.Clear();
        _nextId = 1;
    }
}
