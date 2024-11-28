using System.ComponentModel.DataAnnotations;

namespace DataAccess.Net.DataObject;

public class Room
{
    [Key] public int Id { get; set; }
    public string? Name{get;set;}
    public string? Type{get;set;}
}
