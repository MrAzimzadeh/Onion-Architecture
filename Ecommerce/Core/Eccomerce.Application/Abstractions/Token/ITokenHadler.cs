namespace Ecomerce.Application.Abstractions.Token;

public interface ITokenHadler
{
    DTOs.Token CreateAccessToken(string key, string value); // jwt token acces tokende sayilir 
}