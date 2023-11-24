namespace Task_3_Deep_cloning;

public class Program
{
    public static void Main()
    {
        List<Employee> employees = new List<Employee>()
        {
            new("Mike"),
            new("Susan")
        };

        var department = new Department("Sales", employees);
        var departmentShallowCopy = department.ShallowCopy();
        var departmentDeepCopy = department.DeepCopy();

        Console.WriteLine("Initial department:");
        PrintEmployees(department);

        Console.WriteLine("Shallow clone department:");
        PrintEmployees(departmentShallowCopy);

        Console.WriteLine("Deep clone department:");
        PrintEmployees(departmentDeepCopy);

        Console.WriteLine("\nAdd employees to initial department\n");
        department.Employees.Add(new Employee("Kiel"));
        department.Employees.Add(new Employee("Kris"));

        Console.WriteLine("Initial department (employees added):");
        PrintEmployees(department);

        Console.WriteLine("Shallow clone department (employees added):");
        PrintEmployees(departmentShallowCopy);

        Console.WriteLine("Deep clone department (employees remain the same):");
        PrintEmployees(departmentDeepCopy);
    }

    private static void PrintEmployees(Department department)
    {
        for (int i = 0; i < department.Employees.Count; i++)
        {
            Console.WriteLine($"\t{i}: {department.Employees[i].EmployeeName}");
        }
    }
}