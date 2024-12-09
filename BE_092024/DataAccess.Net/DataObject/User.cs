namespace DataAccess.Net.DataObject;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string? PassWord { get; set; }
    public int? isAdmin { get; set; }
    public DateTime? TokenExpired { get; set; }
    public String? RefreshToken { get; set; }
}