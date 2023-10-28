using Microsoft.Extensions.DependencyInjection;
using OOP.DocumentTypes;
using OOP.Enums;
using OOP.Interfaces;

namespace OOP;

public class Program
{
    public static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddMemoryCache()
            .AddTransient<IDocumentRepository, FileDocumentRepository>()
            .AddSingleton<DocumentService>()
            .BuildServiceProvider();

        var documentService = serviceProvider.GetRequiredService<DocumentService>();

        AddInitializationDocuments(documentService);

        var cardByNumber = documentService.SearchByCardNumber(1);
        var cardsByTitleInfo = documentService.SearchByInfo(EDocumentInfo.Title, "Localized");
        var cardsByPublishDateInfo = documentService.SearchByInfo(EDocumentInfo.PublishDate, "2001-01-01 12:00:00 AM");
        var cardsByUniqueId = documentService.SearchByInfo(EDocumentInfo.UniqueId, "12345678");

        PrintCardsInfo("Documents found by document number",new List<IDocumentCard>{ cardByNumber });
        PrintCardsInfo("Documents found by Title info", cardsByTitleInfo.ToList());
        PrintCardsInfo("Documents found by PublishDate info", cardsByPublishDateInfo.ToList());
        PrintCardsInfo("Documents found by UniqueId info", cardsByUniqueId.ToList());
    }

    private static void AddInitializationDocuments(DocumentService documentService)
    {
        var book = new Book(
            1,
            1,
            "Book Title",
            "BookAuthor1, BookAuthor2",
            new DateTime(2001, 1, 1),
            50,
            "BookPublisher"
        );

        var localizedBook = new LocalizedBook(
            2,
            2,
            "Localized Book Title",
            "LocalizedBookAuthor1, LocalizedBookAuthorAuthor2",
            new DateTime(2001, 1, 1),
            50,
            "LocalPublisher",
            "OriginalPublisher",
            "LocalizationCountry"
        );

        var patent = new Patent(
            3,
            "Patent Title",
            "PatentAuthor1, PatentAuthorAuthor2",
            new DateTime(2003, 3, 3),
            new DateTime(2023, 3, 3),
            12345678
        );

        var magazine = new Magazine(
            4,
            "Magazine Title",
            "MagazinePublisher",
            1,
            new DateTime(2004, 4, 4)
        );

        documentService.AddDocument(book);
        documentService.AddDocument(localizedBook);
        documentService.AddDocument(patent);
        documentService.AddDocument(magazine);
    }

    private static void PrintCardsInfo(string header, List<IDocumentCard> cards)
    {
        Console.WriteLine(header + $"(Document count {cards.Count}:)");
        foreach (var card in cards)
        {
            Console.WriteLine($"Document type: {card.DocumentType}");
            foreach (var info in card.InfoDict)
            {
                Console.WriteLine($"\t{info.Key}: {info.Value}");
            }
        }

        Console.WriteLine("\n");
    }
}