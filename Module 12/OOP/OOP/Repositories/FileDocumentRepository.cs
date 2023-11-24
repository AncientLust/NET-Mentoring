using OOP.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using OOP.Enums;

namespace OOP;

internal class FileDocumentRepository : IDocumentRepository
{
    private const string FilePath = "JsonCards/";
    private const string SearchPattern = "*.json";
    private readonly JsonSerializerSettings _jsonSettings;
    private readonly IMemoryCache _cache;

    public FileDocumentRepository(IMemoryCache cache)
    {
        _cache = cache;
        _jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        
        if (!Directory.Exists(FilePath))
            Directory.CreateDirectory(FilePath);
    }

    public void Add(IDocumentCard documentCard)
    {
        var fileFullPath = FilePath + $"{documentCard.DocumentType}_#{documentCard.CardNumber}.json";

        var jsonString = JsonConvert.SerializeObject(documentCard, Formatting.Indented, _jsonSettings);
        File.WriteAllText(fileFullPath, jsonString);

        CacheDocumentIfCacheable(documentCard, jsonString, fileFullPath);
    }

    private void CacheDocumentIfCacheable(IDocumentCard card, string jsonString, string fullFilePath)
    {
        if (card is ICacheable cacheable)
        {
            var cacheOptions = cacheable.CacheOption;
            _cache.Set(fullFilePath, jsonString, cacheOptions);
        }
    }

    public IDocumentCard? SearchByCardNumber(ulong cardNumber)
    {
        foreach (var file in Directory.GetFiles(FilePath, SearchPattern))
        {
            var jsonString = _cache.TryGetValue(file, out string? cachedJson) ? cachedJson : File.ReadAllText(file);
            if (jsonString == null) continue;

            var documentCard = JsonConvert.DeserializeObject<IDocumentCard>(jsonString, _jsonSettings);

            if (documentCard?.CardNumber == cardNumber)
            {
                CacheDocumentIfCacheable(documentCard, jsonString, file);
                return documentCard;
            }
        }
        return null;
    }

    public IEnumerable<IDocumentCard> SearchByCardInfo(EDocumentInfo info, string value)
    {
        foreach (var file in Directory.GetFiles(FilePath, SearchPattern))
        {
            var jsonString = _cache.TryGetValue(file, out string? cachedJson) ? cachedJson : File.ReadAllText(file);
            if (jsonString == null) continue;

            var parsedJson = JObject.Parse(jsonString);
            var cardInfo = parsedJson[info.ToString()]?.ToString();

            if (cardInfo == null || !cardInfo.Contains(value, StringComparison.OrdinalIgnoreCase)) continue;

            var documentCard = JsonConvert.DeserializeObject<IDocumentCard>(jsonString, _jsonSettings);
            if (documentCard is null) continue;

            CacheDocumentIfCacheable(documentCard, jsonString, file);
            yield return documentCard;
        }
    }
}
