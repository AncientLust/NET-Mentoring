using OOP.Interfaces;
using Newtonsoft.Json;

namespace OOP;

internal class FileDocumentRepository : IDocumentRepository
{
    private string filePath = "JsonCards/";
    private JsonSerializerSettings jsonSettings = new();

    public FileDocumentRepository()
    {
        jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
    }

    public List<DocumentCard> Load()
    {
        List<DocumentCard> result = new List<DocumentCard>();
        
        if (!Directory.Exists(filePath))
            return result;

        foreach (var file in Directory.GetFiles(filePath, "*.json"))
        {
            var jsonString = File.ReadAllText(file);
            var documentCard = JsonConvert.DeserializeObject<DocumentCard>(jsonString, jsonSettings);

            if (documentCard is not null)
                result.Add(documentCard);
        }

        return result;
    }

    public void Add(DocumentCard documentCard)
    {
        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        var fileFullPath = filePath + $"{documentCard.Type}_#{documentCard.CardNumber}.json";

        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        var jsonString = JsonConvert.SerializeObject(documentCard, Formatting.Indented, jsonSettings);
        File.WriteAllText(fileFullPath, jsonString);
    }
}
