using System.Xml.Serialization;
using BYT_Project;

[Serializable]
public class SalesAssociate : Employee
{
    private float _salesCommission;

    private static readonly List<SalesAssociate> _salesAssociateExtent = new List<SalesAssociate>();

    public float SalesCommission
    {
        get => _salesCommission;
        set
        {
            if (value < 0)
                throw new ArgumentException("SalesCommission cannot be negative.");
            _salesCommission = value;
        }
    }

    public SalesAssociate()
    {
        
    }

    public SalesAssociate(Employee employee, float salesCommission)
        : base(employee.Name, "SalesAssociate", employee.Salary)
    {
        SalesCommission = salesCommission;
        AddToExtent(this);
    }
    
    public static Manager SwitchFrom(Employee employee, string storeArea)
    {
        var manager = new Manager(employee, storeArea);
        RemoveFromExtent(employee);
        Manager.SwitchToManagerExtent(manager);
        return manager;
    }

    private static void AddToExtent(SalesAssociate salesAssociate)
    {
        if (salesAssociate == null)
            throw new ArgumentException("SalesAssociate cannot be null.");
        _salesAssociateExtent.Add(salesAssociate);
    }

    public static IReadOnlyList<SalesAssociate> GetExtent() => _salesAssociateExtent.AsReadOnly();

    public static void SaveExtent(string path = "salesAssociates.xml")
    {
        using (StreamWriter file = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SalesAssociate>));
            xmlSerializer.Serialize(file, _salesAssociateExtent);
        }
    }

    public static bool LoadExtent(string path = "salesAssociates.xml")
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SalesAssociate>));
                _salesAssociateExtent.Clear();
                _salesAssociateExtent.AddRange((List<SalesAssociate>)xmlSerializer.Deserialize(file));
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
        _salesAssociateExtent.Clear();
    }

    public static void SwitchToSalesAssociateExtent(SalesAssociate salesAssociate)
    {
        AddToExtent(salesAssociate);
    }
    
    
}
