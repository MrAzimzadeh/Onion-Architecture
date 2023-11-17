using Ecomerce.Application.DTOs;

namespace Ecomerce.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandResponse
{
}

public class LoginUserSuccesCommandResponse : LoginUserCommandResponse
{
    public Token Token { get; set; }
}

public class LoginUserFailCommandResponse : LoginUserCommandResponse
{
    public string Message { get; set; }
}