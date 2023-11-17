namespace Ecomerce.Application.Abstractions.Token;

public interface ITokenHadler
{
    DTOs.Token CreateAccessToken(int minute); // jwt token acces tokende sayilir 
}