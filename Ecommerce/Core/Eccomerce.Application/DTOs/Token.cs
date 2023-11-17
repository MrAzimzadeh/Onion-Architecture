namespace Ecomerce.Application.DTOs;

public class Token
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; } // token omru 
    
}