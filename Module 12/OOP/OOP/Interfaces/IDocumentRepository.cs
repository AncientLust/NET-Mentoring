using OOP.Enums;

namespace OOP.Interfaces;

internal interface IDocumentRepository
{
    public void Add(IDocumentCard documentCard);
    public IDocumentCard? SearchByCardNumber(ulong cardNumber);
    public IEnumerable<IDocumentCard> SearchByCardInfo(EDocumentInfo info, string value);
}
