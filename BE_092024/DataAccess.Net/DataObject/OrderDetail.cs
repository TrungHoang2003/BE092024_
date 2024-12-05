namespace DataAccess.Net.DataObject;

public class OrderDetail
{
    private int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int ProducId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public Order Order { get; set; }
}