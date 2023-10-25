using System.Xml.Serialization;

namespace Task_1_XML_serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var filePath = "Department.xml";

        List<Employee> employees = new List<Employee>()
        {
            new("Mike"),
            new("Susan"),
            new("Stan")
        };

        var department = new Department("Sales", employees);

        SerializeToFile(filePath, department);

        var deserializedDepartment = DeserializeFromFile<Department>(filePath);

        Console.WriteLine($"Department name: {deserializedDepartment.DepartmentName}");
        foreach (var employee in deserializedDepartment.Employees)
        {
            Console.WriteLine($"Employee name: {employee.EmployeeName}");
        }
    }

    public static void SerializeToFile(string filePath, object obj)
    {
        XmlSerializer serializer = new XmlSerializer(obj.GetType());
        using var writer = new StreamWriter(filePath);
        serializer.Serialize(writer, obj);
    }

    public static T DeserializeFromFile<T>(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using var reader = new StreamReader(filePath);
        return (T)serializer.Deserialize(reader);
    }
}