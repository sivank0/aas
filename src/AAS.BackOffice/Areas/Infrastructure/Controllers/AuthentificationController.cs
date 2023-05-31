#region

using System.Net;
using AAS.BackOffice.Controllers;
using AAS.BackOffice.Infrastructure;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;
using PMS.Tools.Managers;

#endregion

namespace AAS.BackOffice.Areas.Infrastructure.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IUsersAuthentificationService _usersAuthentificationService;

    public AuthenticationController(IUsersAuthentificationService usersAuthentificationService)
    {
        _usersAuthentificationService = usersAuthentificationService;
    }

    [HttpGet("/authentication")]
    public ActionResult Authentication()
    {
        string? token = CookieManager.Read(Request, CookieNames.Token);

        if (string.IsNullOrWhiteSpace(token)) return ReactApp();

        Result authenticationResult = _usersAuthentificationService.Authenticate(token);

        if (!authenticationResult.IsSuccess) return ReactApp();

        return Redirect("/");
    }

    [HttpPost("authentication/register_user")]
    public Result RegisterUser([FromBody] UserRegistrationBlank userRegistrationBlank)
    {
        DataResult<UserToken?>
            registrationResult =
                _usersAuthentificationService.RegisterUser(
                    userRegistrationBlank);

        if (!registrationResult.IsSuccess) return Result.Fail(registrationResult.Errors[0].Message);

        Result authenticationResult = _usersAuthentificationService.Authenticate(registrationResult.Data!.Token);

        if (registrationResult.IsSuccess && !authenticationResult.IsSuccess)
            return Result.Fail("Подтвердите почту, чтобы пользоваться сервисом");

        CookieManager.Write(Response, new Cookie(CookieNames.Token, registrationResult.Data!.Token), DateTime.MaxValue);
        return Result.Success();
    }

    public record UserAuthenticationRequest(string? Email, string? Password);

    [HttpPost("authentication/log_in")]
    public DataResult<string?> LogIn([FromBody] UserAuthenticationRequest userAuthenticationRequest)
    {
        string? oldToken = CookieManager.Read(Request, CookieNames.Token);

        if (!string.IsNullOrWhiteSpace(oldToken)) return DataResult<string?>.Success(oldToken);

        DataResult<UserToken?> authentificationResult =
            _usersAuthentificationService.LogIn(userAuthenticationRequest.Email, userAuthenticationRequest.Password);

        if (!authentificationResult.IsSuccess)
            return DataResult<string?>.Fail(authentificationResult.Errors[0].Message);

        if (string.IsNullOrWhiteSpace(authentificationResult.Data!.Token))
            return DataResult<string?>.Success(authentificationResult.Data.Token);

        CookieManager.Write(Response, new Cookie(CookieNames.Token, authentificationResult.Data.Token),
            DateTime.MaxValue);
        return DataResult<string?>.Success(authentificationResult.Data.Token);
    }

    [HttpPost("authentication/log_out")]
    public Result LogOut()
    {
        string? token = CookieManager.Read(Request, CookieNames.Token);

        if (token is null) return Result.Fail("Токен не найден");

        _usersAuthentificationService.LogOut(token);
        CookieManager.Delete(Response, CookieNames.Token);
        return Result.Success();
    }
}