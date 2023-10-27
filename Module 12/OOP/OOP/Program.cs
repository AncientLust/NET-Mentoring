using OOP.InfoAttributes;
using OOP.Interfaces;

namespace OOP;

public class Program
{
    public static void Main()
    {
        var documentRepository = new FileDocumentRepository();
        var documentService = new DocumentService(documentRepository);

        if (documentService.DocumentCount == 0)
            InitDocumentService(documentService);

        var documents = documentService.GetDocumentCards();
        foreach (var document in documents)
        {
            Console.WriteLine($"Type: {document.Type}");
            Console.WriteLine($"Card number: {document.CardNumber}");

            foreach (var info in document.InformationDict)
            {
                Console.WriteLine($"\tInfo key: {info.Key} \tinfo value:{info.Value.InfoStringValue}");
            }
        }
    }

    private static void InitDocumentService(DocumentService documentService)
    {
        List<IInfo> book2Attributes = new()
        {
            new Title("Book title 1"),
            new Authors(new List<string> {"Author1", "Author2"})
        };
        var book2 = new DocumentCard(1, EDocumentType.Book, book2Attributes);

        documentService.AddDocument(book2);
    }
}