using System.Runtime.Serialization.Formatters.Binary;

namespace Task_1_Binary_serialization;

public class Program
{
    public static void Main(string[] args)
    {
        const string filePath = "Department.bin";

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
        using var fileStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        #pragma warning disable SYSLIB0011 // Type or member is obsolete
        formatter.Serialize(fileStream, obj);
        #pragma warning restore SYSLIB0011 // Type or member is obsolete
    }

    public static T DeserializeFromFile<T>(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();

        #pragma warning disable SYSLIB0011 // Type or member is obsolete
        return (T)formatter.Deserialize(fileStream);
        #pragma warning restore SYSLIB0011 // Type or member is obsolete
    }
}