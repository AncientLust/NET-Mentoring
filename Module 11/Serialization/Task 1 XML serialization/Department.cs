using System.Xml.Serialization;

namespace Task_1_XML_serialization;

[XmlRoot(nameof(Department))]
public class Department
{
    [XmlElement(nameof(DepartmentName))]
    public string DepartmentName { get; set; }
    
    [XmlElement(nameof(Employees))]
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