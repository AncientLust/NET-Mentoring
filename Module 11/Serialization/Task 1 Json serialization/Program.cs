using System.Text.Json;

namespace Task_1_Json_serialization;

public class Program
{
    public static void Main(string[] args)
    {
        const string filePath = "Department.json";

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
        var jsonString = JsonSerializer.Serialize(obj);
        File.WriteAllText(filePath, jsonString );
    }

    public static T DeserializeFromFile<T>(string filePath)
    {

        var options = new JsonSerializerOptions
        {
            IncludeFields = true,
        };

        var jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(jsonString, options);
    }
}