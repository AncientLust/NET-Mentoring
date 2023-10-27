namespace OOP.DocumentTypes;

internal class Patent
{
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public DateTime PublishDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public ulong UniqueId { get; set; }
}
