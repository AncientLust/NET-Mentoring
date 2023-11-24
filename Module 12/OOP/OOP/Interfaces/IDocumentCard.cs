using OOP.Enums;

namespace OOP.Interfaces;

internal interface IDocumentCard
{
    public EDocumentType DocumentType { get; }
    public ulong CardNumber { get; }
    public Dictionary<EDocumentInfo, string> InfoDict { get; set; }
}
