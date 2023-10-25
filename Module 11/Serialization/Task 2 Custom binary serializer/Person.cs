using System.Runtime.Serialization;

namespace Task_2_Custom_binary_serializer;

[Serializable]
public class Person : ISerializable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    private Person(SerializationInfo info, StreamingContext context)
    {
        FirstName = info.GetString("FirstName") ?? string.Empty;
        LastName = info.GetString("LastName") ?? string.Empty;
        Age = info.GetInt32("Age");
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("FirstName", FirstName);
        info.AddValue("LastName", LastName);
        info.AddValue("Age", Age);
    }
}