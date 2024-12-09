using System.ComponentModel.DataAnnotations;
using Common.DataValidation;

namespace DataAccess.Net.DataObject;

public class Order
{
    [Key]
    public int OrderId{ get; set; }
    
    [StringValidate]
    public String CustomerName{ get; set; }
    
    public DateTime OrderDate{ get; set; }
    
    public Decimal TotalAmount{ get; set; }
    
    [StringValidate]
    public string Status{ get; set; }
    public ICollection<OrderDetail> OrderDetail{ get; set; }
}