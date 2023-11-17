using System.Security.Authentication;
using Ecomerce.Application.Abstractions.Token;
using Ecomerce.Application.DTOs;
using Ecomerce.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecomerce.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    readonly UserManager<Ecommerce.Domain.Entities.Identity.AppUser> _userManager;
    readonly SignInManager<Ecommerce.Domain.Entities.Identity.AppUser> _signInManager;
    readonly ITokenHadler _tokenHadler;

    public LoginUserCommandHandler(UserManager<Ecommerce.Domain.Entities.Identity.AppUser> userManager,
        SignInManager<Ecommerce.Domain.Entities.Identity.AppUser> signInManager, ITokenHadler tokenHadler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHadler = tokenHadler;
    }

    public async Task<LoginUserCommandResponse>
        Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        Ecommerce.Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        if (user == null)
            throw new NotFoundUserException("UserName or Email is not found");
        
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            Token token = _tokenHadler.CreateAccessToken(5);
            return new LoginUserSuccesCommandResponse()
            {
                Token = token
            };
        }

        throw new AuthenticationErrorException();
    }
}