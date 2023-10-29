namespace ADONET.Models;

internal class Order
{
    public int Id { get; set; }
    public string Status { get; set; }
    public DateTime CreateDate { get; set; }
    public int ProductId { get; set; }
}
