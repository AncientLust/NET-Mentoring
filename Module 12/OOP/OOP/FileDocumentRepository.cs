using OOP.Interfaces;
using System.Text.Json;

namespace OOP;

internal class FileDocumentRepository : IDocumentRepository
{
    private string filePath = "JsonCards/";

    public List<DocumentCard> Load()
    {
        List<DocumentCard> result = new List<DocumentCard>();
        
        if (!Directory.Exists(filePath))
            return result;

        foreach (var file in Directory.GetFiles(filePath, "*.json"))
        {
            var jsonString = File.ReadAllText(file);
            var documentCard = JsonSerializer.Deserialize<DocumentCard>(jsonString);

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
        var jsonString = JsonSerializer.Serialize(documentCard);
        File.WriteAllText(fileFullPath, jsonString);
    }
}
