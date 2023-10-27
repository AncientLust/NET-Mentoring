using OOP.Interfaces;

namespace OOP;

internal class DocumentCard
{
    public ulong CardNumber { get; set; }
    public EDocumentType Type { get; set; }
    public Dictionary<string, IInfo> InformationDict { get; set; } = new ();
    
    public DocumentCard() {}

    public DocumentCard(ulong cardNumber, EDocumentType documentType, List<IInfo> infoAttribute)
    {
        PopulateInformationDict(infoAttribute);
        CardNumber = cardNumber;
        Type = documentType;
    }

    private void PopulateInformationDict(List<IInfo> infoAttribute)
    {
        foreach (var attribute in infoAttribute)
        {
            if (InformationDict.ContainsKey(attribute.InfoName))
                throw new ArgumentException($"Information attributes must be unique. Several entries of {nameof(attribute)} were passed");

            InformationDict.Add(attribute.InfoName, attribute);
        }
    }
}

