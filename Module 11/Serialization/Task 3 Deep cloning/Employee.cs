namespace Task_3_Deep_cloning;

[Serializable]
public class Employee
{
    public string EmployeeName { get; set; }

    public Employee(string name)
    {
        EmployeeName = name;
    }
}

