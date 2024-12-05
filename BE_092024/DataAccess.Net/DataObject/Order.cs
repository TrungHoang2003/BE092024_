using System.ComponentModel.DataAnnotations;
using Common.DataValidation;

namespace DataAccess.Net.DataObject;

public class Order
{
    [Key]
    public int OrderId{ get; set; }
    
    [StringValidate]
    public String CustomerName{ get; set; }
    
    [DateValidate]
    public DateTime OrderDate{ get; set; }
    
    [NumberValidate]
    public Decimal TotalAmount{ get; set; }
    
    [StringValidate]
    public string Status{ get; set; }
    public ICollection<OrderDetail> OrderDetail{ get; set; }
}