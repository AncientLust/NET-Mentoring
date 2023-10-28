using Microsoft.Extensions.Caching.Memory;
using OOP.Enums;
using OOP.Interfaces;

namespace OOP.DocumentTypes;

internal class Magazine : IDocumentCard, ICacheable
{
    public EDocumentType DocumentType { get; set; } 
    public ulong CardNumber { get; set; }
    public Dictionary<EDocumentInfo, string> InfoDict { get; set; } = new();
    public string Title { get; set; }
    public string Publisher { get; set; }
    public ulong ReleaseNumber { get; set; }
    public DateTime PublishDate { get; set; }
    public MemoryCacheEntryOptions CacheOption { get; set; }

    public Magazine(ulong cardNumber, string title, string publisher, 
        ulong releaseNumber, DateTime publishDate)
    {
        DocumentType = EDocumentType.Magazine;
        CardNumber = cardNumber;
        
        Title = title;
        Publisher = publisher;
        ReleaseNumber = releaseNumber;
        PublishDate = publishDate;

        InfoDict.Add(EDocumentInfo.Title, title);
        InfoDict.Add(EDocumentInfo.Publisher, publisher);
        InfoDict.Add(EDocumentInfo.ReleaseNumber, releaseNumber.ToString());
        InfoDict.Add(EDocumentInfo.PublishDate, publishDate.ToString("s"));

        CacheOption = new MemoryCacheEntryOptions
        {
            Priority = CacheItemPriority.NeverRemove
        };
    }
}
