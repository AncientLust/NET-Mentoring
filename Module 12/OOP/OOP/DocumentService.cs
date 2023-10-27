using OOP.Interfaces;

namespace OOP;

internal class DocumentService
{
    private List<DocumentCard> storage;
    private readonly IDocumentRepository documentRepository;

    public int DocumentCount => storage.Count;

    public DocumentService(IDocumentRepository storageProvider)
    {
        this.documentRepository = storageProvider;
        storage = this.documentRepository.Load();
    }

    public void AddDocument(DocumentCard documentCard)
    {
        documentRepository.Add(documentCard);
        storage.Add(documentCard);
    }

    public List<int> SearchDocument(int docNumber)
    {
        throw new NotImplementedException();
    }

    public List<DocumentCard> GetDocumentCards()
    {
        return storage;
    }
}
