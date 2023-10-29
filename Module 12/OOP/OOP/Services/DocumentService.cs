using OOP.Enums;
using OOP.Interfaces;

namespace OOP.Services;

internal class DocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public void AddDocument(IDocumentCard documentCard)
    {
        _documentRepository.Add(documentCard);
    }

    public IDocumentCard? SearchByCardNumber(ulong docNumber)
    {
        return _documentRepository.SearchByCardNumber(docNumber);
    }

    public IEnumerable<IDocumentCard> SearchByInfo(EDocumentInfo info, string value)
    {
        return _documentRepository.SearchByCardInfo(info, value);
    }
}
