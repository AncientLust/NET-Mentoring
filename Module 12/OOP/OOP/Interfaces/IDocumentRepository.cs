namespace OOP.Interfaces;

internal interface IDocumentRepository
{
    public List<DocumentCard> Load();
    public void Add(DocumentCard documentCard);
}
