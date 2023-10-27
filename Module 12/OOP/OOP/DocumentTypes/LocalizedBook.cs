namespace OOP.DocumentTypes;

internal class LocalizedBook
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public DateTime PublishDate { get; set; }
    public int NumberOfPages { get; set; }
    public string LocalPublisher { get; set; }
    public string OriginalPublisher { get; set; }
    public string LocalizationCountry { get; set; }
}

