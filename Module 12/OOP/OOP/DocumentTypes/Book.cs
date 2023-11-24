using Microsoft.Extensions.Caching.Memory;
using OOP.Enums;
using OOP.Interfaces;

namespace OOP.DocumentTypes;

internal class Book : IDocumentCard, ICacheable
{
    public EDocumentType DocumentType { get; set; }
    public ulong CardNumber { get; set; }
    public Dictionary<EDocumentInfo, string> InfoDict { get; set; } = new ();
    public ulong Isbn { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public DateTime PublishDate { get; set; }
    public int NumberOfPages { get; set; }
    public string Publisher { get; set; }
    public MemoryCacheEntryOptions CacheOption { get; set; }

    public Book(ulong cardNumber, ulong isbn, string title, string authors,
        DateTime publishDate, int numberOfPages, string publisher)
    {
        CardNumber = cardNumber;
        DocumentType = EDocumentType.Book;

        Isbn = isbn;
        Title = title;
        Authors = authors;
        PublishDate = publishDate;
        NumberOfPages = numberOfPages;
        Publisher = publisher;

        InfoDict.Add(EDocumentInfo.Isbn, isbn.ToString());
        InfoDict.Add(EDocumentInfo.Title, title);
        InfoDict.Add(EDocumentInfo.Authors, authors);
        InfoDict.Add(EDocumentInfo.PublishDate, publishDate.ToString("s"));
        InfoDict.Add(EDocumentInfo.NumberOfPages, numberOfPages.ToString());
        InfoDict.Add(EDocumentInfo.Publisher, publisher);

        CacheOption = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(15)
        };
    }
}

