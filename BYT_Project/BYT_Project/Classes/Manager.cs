using System.Xml.Serialization;

[Serializable]
public class Manager : Employee
{
    private string _storeArea;

    private static readonly List<Manager> _managerExtent = new List<Manager>();

    public string StoreArea
    {
        get => _storeArea;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("StoreArea cannot be null or empty.");
            _storeArea = value;
        }
    }

    public Manager()
    {
        
    }

    public Manager(Employee employee, string storeArea)
        : base(employee.Name, "Manager", employee.Salary)
    {
        StoreArea = storeArea;
        AddToExtent(this);
    }
    
    public static SalesAssociate SwitchFrom(Employee employee, float salesCommission)
    {
        SalesAssociate salesAssociate = new SalesAssociate(employee, salesCommission);
    
        RemoveFromExtent(employee);

        SalesAssociate.SwitchToSalesAssociateExtent(salesAssociate);
    
        return salesAssociate;
    }

    private static void AddToExtent(Manager manager)
    {
        if (manager == null)
            throw new ArgumentException("Manager cannot be null.");
        _managerExtent.Add(manager);
    }

    public static IReadOnlyList<Manager> GetExtent() => _managerExtent.AsReadOnly();

    public static void SaveExtent(string path = "managers.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Manager>));
            xmlSerializer.Serialize(file, _managerExtent);
        }
    }

    public static bool LoadExtent(string path = "managers.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Manager>));
                _managerExtent.Clear();
                _managerExtent.AddRange((List<Manager>)xmlSerializer.Deserialize(file));
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
        _managerExtent.Clear();
    }
    
    public static void SwitchToManagerExtent(Manager manager)
    {
        AddToExtent(manager);
    }

}