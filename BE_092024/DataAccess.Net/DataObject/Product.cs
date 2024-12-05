using System.ComponentModel.DataAnnotations;
using Common.DataValidation;

namespace DataAccess.Net.DataObject;

public class Product
{
    [Key]
    public int ProductId{get;set;}
    
    [StringValidate]
    public string? ProductName{get;set;}
    
    [NumberValidate]
    public decimal Price{get;set;}
    
    [NumberValidate]
    public int Stock{get;set;}
}
