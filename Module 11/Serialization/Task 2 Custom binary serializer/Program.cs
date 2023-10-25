using System.Runtime.Serialization.Formatters.Binary;

namespace Task_2_Custom_binary_serializer;

public class Program
{
    public static void Main(string[] args)
    {
        const string filePath = "Department.bin";

        var person = new Person("Emily", "Jane", 23);

        SerializeToFile(filePath, person);

        var deserializedPerson = DeserializeFromFile<Person>(filePath);

        Console.WriteLine($"First name: {deserializedPerson.FirstName}");
        Console.WriteLine($"Last name: {deserializedPerson.LastName}");
        Console.WriteLine($"Age: {deserializedPerson.Age}");
    }

    public static void SerializeToFile(string filePath, object obj)
    {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        #pragma warning disable SYSLIB0011 // Type or member is obsolete
        formatter.Serialize(fileStream, obj);
        #pragma warning restore SYSLIB0011 // Type or member is obsolete
    }

    public static T DeserializeFromFile<T>(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();

        #pragma warning disable SYSLIB0011 // Type or member is obsolete
        return (T)formatter.Deserialize(fileStream);
        #pragma warning restore SYSLIB0011 // Type or member is obsolete
    }
}