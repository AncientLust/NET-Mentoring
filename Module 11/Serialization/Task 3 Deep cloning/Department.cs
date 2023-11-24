using System.Runtime.Serialization.Formatters.Binary;

namespace Task_3_Deep_cloning;

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

    public Department DeepCopy()
    {
        using MemoryStream stream = new MemoryStream();
        if (GetType().IsSerializable)
        {
            BinaryFormatter formatter = new BinaryFormatter();
                
            #pragma warning disable SYSLIB0011 // Type or member is obsolete
            formatter.Serialize(stream, this);
            #pragma warning restore SYSLIB0011 // Type or member is obsolete

            stream.Position = 0;

            #pragma warning disable SYSLIB0011 // Type or member is obsolete
            return (Department)formatter.Deserialize(stream);
            #pragma warning restore SYSLIB0011 // Type or member is obsolete
        }
        return null;
    }

    public Department ShallowCopy()
    {
        return (Department)MemberwiseClone();
    }
}