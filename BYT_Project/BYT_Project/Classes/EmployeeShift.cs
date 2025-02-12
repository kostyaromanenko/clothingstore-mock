using System;
using System.Xml.Serialization;

[Serializable]
public class EmployeeShift
{
    private static int _nextId = 1;
    private int _shiftId;

    private DateTime _shiftDate;
    private TimeSpan _startTime;
    private TimeSpan _endTime;

    private Employee _employee;
    private Store _store;
    private string _role;

    private static readonly List<EmployeeShift> _shiftExtent = new List<EmployeeShift>();

    public int ShiftId
    {
        get => _shiftId;
        set => _shiftId = value;
    }

    public DateTime ShiftDate
    {
        get => _shiftDate;
        set
        {
            if (value == default)
                throw new ArgumentException("ShiftDate cannot be empty.");
            _shiftDate = value;
        }
    }

    public TimeSpan StartTime
    {
        get => _startTime;
        set
        {
            if (value == default)
                throw new ArgumentException("StartTime cannot be empty.");
            _startTime = value;
        }
    }

    public TimeSpan EndTime
    {
        get => _endTime;
        set
        {
            if (value == default)
                throw new ArgumentException("EndTime cannot be empty.");
            if (value <= _startTime)
                throw new ArgumentException("EndTime must be after StartTime.");
            _endTime = value;
        }
    }

    public Employee Employee => _employee;
    public Store Store => _store;      
    public string Role => _role;          

    public EmployeeShift()
    {
        
    }
    
    public EmployeeShift(DateTime shiftDate, TimeSpan startTime, TimeSpan endTime, Employee employee, Store store, string role)
    {
        _shiftId = _nextId++;
        ShiftDate = shiftDate;
        StartTime = startTime;
        EndTime = endTime;

        SetEmployee(employee);
        SetStore(store);
        _role = role;

        AddToExtent(this);
    }

    public void SetEmployee(Employee employee)
    {
        if (_employee != null && _employee != employee)
        {
            _employee.RemoveShift(this);
        }
        _employee = employee;
        if (_employee != null)
        {
            _employee.AddShift(this);
        }
    }
    
    
    public void SetStore(Store store)
    {
        
        if (_store != store)
        {
            var previousStore = _store;
            _store = store;
            
            previousStore?.RemoveShift(this);
            store?.AddShift(this);
        }
    }


    
    public static void SaveExtent(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<EmployeeShift>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, _shiftExtent);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving extent: {ex.Message}");
        }
    }
    
        public static bool LoadExtent(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<EmployeeShift>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                _shiftExtent.Clear();
                _shiftExtent.AddRange((List<EmployeeShift>)serializer.Deserialize(reader));
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading extent: {ex.Message}");
            return false;
        }
    }

    private static void AddToExtent(EmployeeShift shift)
    {
        if (shift == null)
            throw new ArgumentException("Shift cannot be null.");
        _shiftExtent.Add(shift);
    }

    public static IReadOnlyList<EmployeeShift> GetExtent() => _shiftExtent.AsReadOnly();

    public static void ClearExtent()
    {
        _shiftExtent.Clear();
        _nextId = 1;
    }
}
