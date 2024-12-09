using System.ComponentModel.DataAnnotations;

namespace DataAccess.Net.DataObject;

public class OrderDetail
{
    [Key] public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalAmount { get; set; }
    
}