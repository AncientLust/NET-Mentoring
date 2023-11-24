namespace Task_1_Binary_serialization;

[Serializable]
public class Department
{
    public string DepartmentName { get; set; }
    public List<Employee> Employees { get; set; }

    public Department(string name, List<Employee> employees)
    {
        DepartmentName = name;
        Employees = employees;
    }
}