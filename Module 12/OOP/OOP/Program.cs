using Microsoft.Extensions.DependencyInjection;
using OOP.Enums;
using OOP.Interfaces;
using OOP.Services;

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
        var dataSource = new DataSource();

        documentService.AddDocument(dataSource.Book);
        documentService.AddDocument(dataSource.LocalizedBook);
        documentService.AddDocument(dataSource.Patent);
        documentService.AddDocument(dataSource.Magazine);
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