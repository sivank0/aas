using AAS.BackOffice.Controllers;
using AAS.BackOffice.Infrastructure;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;
using PMS.Tools.Managers;
using System.Net;

namespace AAS.BackOffice.Areas.Infrastructure.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IUsersService _usersService;

    public AuthenticationController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("/authentication")]
    public ActionResult Authentication()
    {
        String? token = CookieManager.Read(Request, CookieNames.Token);

        if (String.IsNullOrWhiteSpace(token)) return ReactApp();

        Result authenticationResult = _usersService.Authenticate(token);

        if (!authenticationResult.IsSuccess) return ReactApp();

        return Redirect("/");
    }

    [HttpPost("authentication/register_user")]
    public Result RegisterUser([FromBody] UserRegistrationBlank userRegistrationBlank)
    {
        DataResult<UserToken?> registrationResult = _usersService.RegisterUser(userRegistrationBlank); // REFACTORING в DataResult можно добавить атрибут MemberNotNullWhen, чтобы не делать ! или проверку Data на null

        if (!registrationResult.IsSuccess) return Result.Fail(registrationResult.Errors[0].Message);

        Result authenticationResult = _usersService.Authenticate(registrationResult.Data!.Token);

        if (registrationResult.IsSuccess && !authenticationResult.IsSuccess)
            return Result.Fail("Обратитесь к администратору для того, чтобы начать пользоваться сервисом");

        CookieManager.Write(Response, new Cookie(CookieNames.Token, registrationResult.Data!.Token), DateTime.MaxValue);
        return Result.Success();
    }

    public record UserAuthenticationRequest(String? Email, String? Password);

    [HttpPost("authentication/log_in")]
    public DataResult<String?> LogIn([FromBody] UserAuthenticationRequest userAuthenticationRequest)
    {
        String? oldToken = CookieManager.Read(Request, CookieNames.Token);

        if (!String.IsNullOrWhiteSpace(oldToken)) return DataResult<String?>.Success(oldToken);

        DataResult<UserToken?> authentificationResult = _usersService.LogIn(userAuthenticationRequest.Email, userAuthenticationRequest.Password);

        if (!authentificationResult.IsSuccess) return DataResult<String?>.Fail(authentificationResult.Errors[0].Message);

        if (String.IsNullOrWhiteSpace(authentificationResult.Data!.Token))
            return DataResult<String?>.Success(authentificationResult.Data.Token);

        CookieManager.Write(Response, new Cookie(CookieNames.Token, authentificationResult.Data.Token), DateTime.MaxValue);
        return DataResult<String?>.Success(authentificationResult.Data.Token);
    }

    [HttpPost("authentication/log_out")]
    public Result LogOut()
    {
        String? token = CookieManager.Read(Request, CookieNames.Token);

        if (token is null) return Result.Fail("Токен не найден");

        _usersService.LogOut(token);
        CookieManager.Delete(Response, CookieNames.Token);
        return Result.Success();
    }
}
