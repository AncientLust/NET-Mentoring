namespace Task_1_Binary_serialization;

[Serializable]
public class Employee
{
    public string EmployeeName { get; set; }

    public Employee(string name)
    {
        EmployeeName = name;
    }
}

