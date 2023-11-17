using Ecomerce.Application.Abstractions.Token;
using Ecomerce.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecomerce.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    readonly UserManager<Ecommerce.Domain.Entities.Identity.AppUser> _userManager;
    readonly ITokenHadler _tokenHadler;

    public GoogleLoginCommandHandler(UserManager<Ecommerce.Domain.Entities.Identity.AppUser> userManager,
        ITokenHadler tokenHadler)
    {
        _userManager = userManager;
        _tokenHadler = tokenHadler;
    }

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string>() { "303139520379-r7dpsujdc0mnij2acn4aqp7f41d4rkca.apps.googleusercontent.com" }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
        var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
        Ecommerce.Domain.Entities.Identity.AppUser user =
            await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new Ecommerce.Domain.Entities.Identity.AppUser()
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    NameSurname = payload.GivenName + payload.FamilyName,
                    EmailConfirmed = true
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result)
            await _userManager.AddLoginAsync(user, info);
        else
        {
            throw new Exception("Google login failed");
        }

        Token token = _tokenHadler.CreateAccessToken(5);
        return new()
        {
            Token = token
        };
    }
}