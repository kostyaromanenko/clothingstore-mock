using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
[XmlInclude(typeof(Manager))]
[XmlInclude(typeof(SalesAssociate))]
public class Employee
{
    private static int _nextId = 1;
    private int _employeeId;
    private string _name;
    private string _role;
    private float _salary;
    private string? _currency;

    private Employee? _supervisor;
    private readonly List<Employee> _subordinates = new List<Employee>();
    private readonly Dictionary<Store, string> _storesByRole = new Dictionary<Store, string>();

    private readonly List<EmployeeShift> _shifts = new List<EmployeeShift>();

    private static readonly List<Employee> _employeeExtent = new List<Employee>();

    public int EmployeeId
    {
        get => _employeeId;
        set => _employeeId = value;
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

    public string Role
    {
        get => _role;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Role cannot be null or empty.");
            _role = value;
        }
    }

    public float Salary
    {
        get => _salary;
        set
        {
            if (value < 0)
                throw new ArgumentException("Salary cannot be negative.");
            _salary = value;
        }
    }

    public string? Currency
    {
        get => _currency;
        set => _currency = value;
    }

    public Employee? Supervisor => _supervisor;
    public IReadOnlyList<Employee> Subordinates => _subordinates.AsReadOnly();
    public IReadOnlyList<EmployeeShift> Shifts => _shifts.AsReadOnly();
    
    public IReadOnlyDictionary<Store, string> StoresByRole => _storesByRole;


    public Employee()
    {
    }
    

    public Employee(string name, string role, float salary, string? currency = null)
    {
        _employeeId = _nextId++;
        Name = name;
        Role = role;
        Salary = salary;
        Currency = currency;
        AddToExtent(this);
    }
    
    public Employee Copy()
    {
        return new Employee(Name, Role, Salary, _currency) { _employeeId = this.EmployeeId };
    }
    
    public string? GetRoleByStore(Store store)
    {
        _storesByRole.TryGetValue(store, out var role);
        return role;
    }

    public void AddStoreByRole(Store store, string role)
    {
        if (store == null)
            throw new ArgumentException("Store cannot be null.");
        if (string.IsNullOrEmpty(role))
            throw new ArgumentException("Role cannot be null or empty.");

        if (_storesByRole.ContainsKey(store))
            throw new InvalidOperationException($"The employee is already assigned to the store '{store.StoreName}' with a role.");

        _storesByRole[store] = role;
        store.AddEmployeeByRole(role, this);
    }

    public void RemoveStoreByRole(Store store, string role)
    {
        if (store == null)
            throw new ArgumentException("Store cannot be null.");
        if (!_storesByRole.Remove(store))
            throw new InvalidOperationException("The employee is not associated with the store for the given role.");

        store.RemoveEmployeeByRole(role);
    }

    public void UpdateStoreRole(Store store, string oldRole, string newRole)
    {
        RemoveStoreByRole(store, oldRole);
        AddStoreByRole(store, newRole);
    }
    
    
    public void AddSubordinate(Employee subordinate)
    {
        if (subordinate == null)
            throw new ArgumentException("Subordinate cannot be null.");

        if (subordinate == this)
            throw new InvalidOperationException("An employee cannot supervise itself.");

        if (!_subordinates.Contains(subordinate))
        {
            _subordinates.Add(subordinate);
            subordinate.SetSupervisor(this);
        }
    }

    public void RemoveSubordinate(Employee subordinate)
    {
        if (subordinate == null)
            throw new ArgumentException("Subordinate cannot be null.");

        if (_subordinates.Remove(subordinate))
        {
            subordinate.RemoveSupervisor();
        }
    }

    public void SetSupervisor(Employee? supervisor)
    {
        if (supervisor == this)
            throw new InvalidOperationException("An employee cannot supervise itself.");

        if (_supervisor != supervisor)
        {
            _supervisor?.RemoveSubordinate(this);
            _supervisor = supervisor;

            supervisor?.AddSubordinate(this);
        }
    }

    public void RemoveSupervisor()
    {
        if (Supervisor != null)
        {
            Supervisor.RemoveSubordinate(this);
            SetSupervisor(null);
        }
    }


    public void AddShift(EmployeeShift shift)
    {
        if (shift == null)
            throw new ArgumentException("Shift cannot be null.");
        if (!_shifts.Contains(shift))
        {
            _shifts.Add(shift);
            shift.SetEmployee(this);
        }
    }

    public void RemoveShift(EmployeeShift shift)
    {
        if (_shifts.Remove(shift))
        {
            shift.SetEmployee(null);
        }
    }
    
    private static void AddToExtent(Employee employee)
    {
        if (employee == null)
            throw new ArgumentException("Employee cannot be null.");
        _employeeExtent.Add(employee);
    }

    public static IReadOnlyList<Employee> GetExtent() => _employeeExtent.AsReadOnly();

    public static void SaveExtent(string path = "employees.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Employee>));
            xmlSerializer.Serialize(file, _employeeExtent);
        }
    }

    public static bool LoadExtent(string path = "employees.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Employee>));
                _employeeExtent.Clear();
                _employeeExtent.AddRange((List<Employee>)xmlSerializer.Deserialize(file));

                if (_employeeExtent.Count > 0)
                    _nextId = _employeeExtent[^1].EmployeeId + 1;
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
        _employeeExtent.Clear();
        _nextId = 1;
    }
    public static void RemoveFromExtent(Employee employee) => _employeeExtent.Remove(employee);
    
}
