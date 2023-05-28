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
    public ViewResult Index()
    {
        return ReactApp();
    }

    [Route("/error/403")]
    [Route("error/404")]
    public ViewResult Forbidden()
    {
        return ReactApp();
    }

    [Route("/registration")]
    public ViewResult Registration()
    {
        return ReactApp();
    }

    [Route("/users")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public ViewResult Users()
    {
        return ReactApp();
    }

    [Route("/user_profile")]
    [IsAuthorized(AccessPolicy.UserProfile)]
    public ViewResult UserProfile()
    {
        return ReactApp();
    }

    [Route("/user_roles")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public ViewResult UserRoles()
    {
        return ReactApp();
    }

    [Route("/bids")]
    [IsAuthorized(AccessPolicy.BidsRead)]
    public ViewResult Bids()
    {
        return ReactApp();
    }

    [Route("/tests")]
    [IsAuthorized(AccessPolicy.BidsRead)]
    public ViewResult Tests()
    {
        return ReactApp();
    }
}