using OOP.Enums;
using OOP.Interfaces;

namespace OOP.DocumentTypes;

internal class Patent : IDocumentCard
{
    public EDocumentType DocumentType { get; set; } 
    public ulong CardNumber { get; set; }
    public Dictionary<EDocumentInfo, string> InfoDict { get; set; } = new();
    public string Title { get; set; }
    public string Authors { get; set; }
    public DateTime PublishDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public ulong UniqueId { get; set; }

    public Patent(ulong cardNumber, string title, string authors,
        DateTime publishDate, DateTime expirationDate, uint uniqueId)
    {
        CardNumber = cardNumber;
        DocumentType = EDocumentType.Patent;

        Title = title;
        Authors = authors;
        PublishDate = publishDate;
        ExpirationDate = expirationDate;
        UniqueId = uniqueId;

        InfoDict.Add(EDocumentInfo.Title, title);
        InfoDict.Add(EDocumentInfo.Authors, authors);
        InfoDict.Add(EDocumentInfo.PublishDate, publishDate.ToString("s"));
        InfoDict.Add(EDocumentInfo.ExpirationDate, expirationDate.ToString("s"));
        InfoDict.Add(EDocumentInfo.UniqueId, uniqueId.ToString());
    }
}
