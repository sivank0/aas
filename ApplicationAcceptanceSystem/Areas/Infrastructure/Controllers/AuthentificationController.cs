using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.BackOffice.Infrastructure;
using AAS.Domain.Services;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AAS.BackOffice.Areas.Infrastructure.Controllers;

public class AuthenticationController : BaseController
{
	private readonly IUsersService _usersService;

	public AuthenticationController(IUsersService usersService)
	{
		_usersService = usersService;
	}

	//[HttpGet("/authentication")]
	//public ActionResult Authentication()
	//{
	//	String? token = CookieManager.Read(Request, CookieNames.Token);

	//	if (String.IsNullOrWhiteSpace(token)) return ReactApp();

	//	Result authenticationResult = _usersService.Authenticate(token);

	//	if (!authenticationResult.IsSuccess) return ReactApp();

	//	return Redirect("/");
	//}

	//[HttpPost("authentication/register_user")]
	//public async Task<Result> RegisterUser([FromBody] UserRegistrationBlank userRegistrationBlank)
	//{
	//	DataResult<UserToken?> registrationResult = await _usersService.RegisterUser(userRegistrationBlank); // REFACTORING в DataResult можно добавить атрибут MemberNotNullWhen, чтобы не делать ! или проверку Data на null

	//	if (!registrationResult.IsSuccess) return Result.Fail(registrationResult.Errors[0]);

	//	Result authenticationResult = _usersService.Authenticate(registrationResult.Data!.Token);

	//	if (!authenticationResult.IsSuccess) return Result.Fail("Регистрация не удалась, попробуйте ещё раз");

	//	CookieManager.Write(Response, new Cookie(CookieNames.Token, registrationResult.Data!.Token), DateTime.MaxValue);
	//	return Result.Success();
	//}

	//public record UserAuthenticationRequest(String? Email, String? Password, String? GoogleToken);

	//[HttpPost("authentication/log_in")]
	//public async Task<AuthentificationResult> LogIn([FromBody] UserAuthenticationRequest userAuthenticationRequest)
	//{
	//	String? oldToken = CookieManager.Read(Request, CookieNames.Token);

	//	if (!String.IsNullOrWhiteSpace(oldToken)) return AuthentificationResult.Success(oldToken);

	//	AuthentificationResult authentificationResult;

	//	if (userAuthenticationRequest.GoogleToken is not null) authentificationResult = await _usersService.LogIn(userAuthenticationRequest.GoogleToken);
	//	else authentificationResult = await _usersService.LogIn(userAuthenticationRequest.Email, userAuthenticationRequest.Password);

	//	if (!authentificationResult.IsSuccess) return AuthentificationResult.Failed(authentificationResult.Errors[0].Message);

	//	if (String.IsNullOrWhiteSpace(authentificationResult.Token))
	//		return AuthentificationResult.Success(authentificationResult.Token, authentificationResult.Email, authentificationResult.Login,
	//			authentificationResult.FirstName, authentificationResult.LastName);

	//	CookieManager.Write(Response, new Cookie(CookieNames.Token, authentificationResult.Token), DateTime.MaxValue);
	//	return AuthentificationResult.Success(authentificationResult.Token);
	//}

	//[HttpPost("authentication/change_workspace")]
	//[IsAuthorized]
	//public Task<Result> ChangeWorkspace([FromBody] ID workspaceId)
	//{
	//	return _usersService.ChangeSelectedWorkspace(workspaceId, SystemUser);
	//}

	//[HttpPost("authentication/log_out")]
	//public Result LogOut()
	//{
	//	String? token = CookieManager.Read(Request, CookieNames.Token);

	//	if (token is null) return Result.Fail("Токен не найден");

	//	_usersService.LogOut(token);
	//	CookieManager.Delete(Response, CookieNames.Token);
	//	return Result.Success();
	//}
}
