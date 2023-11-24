using Library.Enums;

namespace Library.Models;

public class Order
{
    public int Id { get; set; }
    public EOrderStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int ProductId { get; set; }
}
