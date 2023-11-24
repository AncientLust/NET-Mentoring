using Microsoft.Extensions.Caching.Memory;
using OOP.Enums;
using OOP.Interfaces;

namespace OOP.DocumentTypes;

internal class LocalizedBook : IDocumentCard, ICacheable
{
    public EDocumentType DocumentType { get; set; } 
    public ulong CardNumber { get; set; }
    public Dictionary<EDocumentInfo, string> InfoDict { get; set; } = new();
    public ulong Isbn { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public DateTime PublishDate { get; set; }
    public int NumberOfPages { get; set; }
    public string LocalPublisher { get; set; }
    public string OriginalPublisher { get; set; }
    public string LocalizationCountry { get; set; }
    public MemoryCacheEntryOptions CacheOption { get; set; }

    public LocalizedBook(ulong cardNumber, ulong isbn, string title, string authors,
        DateTime publishDate, int numberOfPages, string localPublisher, string originalPublisher, string localizationCountry)
    {
        CardNumber = cardNumber;
        DocumentType = EDocumentType.LocalizedBook;
        
        Isbn = isbn;
        Title = title;
        Authors = authors;
        PublishDate = publishDate;
        NumberOfPages = numberOfPages;
        LocalPublisher = localPublisher;
        OriginalPublisher = originalPublisher;
        LocalizationCountry = localizationCountry;

        InfoDict.Add(EDocumentInfo.Isbn, isbn.ToString());
        InfoDict.Add(EDocumentInfo.Title, title);
        InfoDict.Add(EDocumentInfo.Authors, authors);
        InfoDict.Add(EDocumentInfo.PublishDate, publishDate.ToString("s"));
        InfoDict.Add(EDocumentInfo.NumberOfPages, numberOfPages.ToString());
        InfoDict.Add(EDocumentInfo.LocalPublisher, localPublisher);
        InfoDict.Add(EDocumentInfo.OriginalPublisher, originalPublisher);
        InfoDict.Add(EDocumentInfo.LocalizationCountry, localizationCountry);

        CacheOption = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(10)
        };
    }
}

