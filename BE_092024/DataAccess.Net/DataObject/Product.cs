using System.ComponentModel.DataAnnotations;
using Common.DataValidation;

namespace DataAccess.Net.DataObject;

public class Product
{
    [Key]
    public int ProductId{get;set;}
    
    [StringValidate]
    public string? ProductName{get;set;}
    
    public decimal Price{get;set;}
    
    public int Stock{get;set;}
}
