#region

using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace AAS.BackOffice.Areas.Infrastructure.Controllers;

public class HomeController : BaseController
{
    [Route("/")]
    [IsAuthorized]
    public ViewResult Index() => ReactApp();

    [Route("/error/403")]
    [Route("error/404")]
    public ViewResult Forbidden() => ReactApp();

    [Route("/registration")]
    public ViewResult Registration() => ReactApp();

    [Route("/email_verification/{token}")]
    public ViewResult EmailVerifications() => ReactApp();

    [Route("/users")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public ViewResult Users() => ReactApp();

    [Route("/user_profile")]
    [IsAuthorized(AccessPolicy.UserProfile)]
    public ViewResult UserProfile() => ReactApp();

    [Route("/user_roles")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public ViewResult UserRoles() => ReactApp();

    [Route("/bids")]
    [IsAuthorized(AccessPolicy.BidsRead)]
    public ViewResult Bids() => ReactApp();

    [Route("/tests")]
    [IsAuthorized(AccessPolicy.BidsRead)]
    public ViewResult Tests() => ReactApp();
}