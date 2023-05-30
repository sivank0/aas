using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Configurator;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Services;
using AAS.Domain.Users.Roles;
using AAS.Tools.Types.IDs;
using Microsoft.AspNetCore.Mvc;

namespace AAS.BackOffice.Areas.Infrastructure.Controllers;

public class UpdatesController : BaseController
{
    private readonly IUsersService _usersService;

    public UpdatesController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("updates/create_default_role")]
    [IsAuthorized(AccessPolicy.UserRolesUpdate)]
    public String CreateDefaultRole()
    {
        UserRoleBlank userRoleBlank = new(
            Configurations.BackOffice.DefaultRoleId,
            "Пользователь",
            new AccessPolicy[] {
                AccessPolicy.UserProfile,
                AccessPolicy.BidsRead
            }
        );

        _usersService.SaveUserRole(userRoleBlank, SystemUser.Id);

        return "Всё прошло успешно";
    }
}
