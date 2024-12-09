using Common.DataValidation;
using DataAccess.Net.DataObject;

namespace DataAccess.Net.DTO;

public class OrderDTO
{
    [StringValidate]
    public String CustomerName{get; set;}
    
    public DateTime OrderDate{get; set;}
    
    [StringValidate]
    public String Status{get; set;}
    
    public ICollection<OrderDetailDTO> OrderDetail{get; set;}
    
}

public class OrderDetailDTO
{
    public int ProductId{get; set;}
    
    public int Quantity{get; set;}
    
}