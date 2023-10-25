namespace Task_1_Json_serialization;

public class Department
{
    public string DepartmentName { get; set; }
    public List<Employee> Employees { get; set; }

    public Department()
    {
        
    }

    public Department(string name, List<Employee> employees)
    {
        DepartmentName = name;
        Employees = employees;
    }
}