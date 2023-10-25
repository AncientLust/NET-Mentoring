using System.Xml.Serialization;

namespace Task_1_XML_serialization;

[XmlRoot(nameof(Employee))]
public class Employee
{
    [XmlElement(nameof(EmployeeName))]
    public string EmployeeName { get; set; }

    public Employee()
    {
        
    }

    public Employee(string name)
    {
        EmployeeName = name;
    }
}

