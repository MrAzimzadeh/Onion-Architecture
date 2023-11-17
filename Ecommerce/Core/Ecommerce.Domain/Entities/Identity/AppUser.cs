using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string NameSurname { get; set; }   
}