using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Ecomerce.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using T = Ecomerce.Application.DTOs;

namespace Ecomerce.Infrastructure.Services.Token;

public class TokenHandler : ITokenHadler
{
    readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public T.Token CreateAccessToken(int minute)
    {
        T.Token token = new();

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.Now.AddMinutes(minute);

        JwtSecurityToken securityToken = new
        (
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
        );
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        
        return token;
    }
}