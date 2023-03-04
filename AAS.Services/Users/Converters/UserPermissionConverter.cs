using AAS.Domain.Users.Permissions;
using AAS.Services.Users.Models;

namespace AAS.Services.Users.Converters;

internal static class UserPermissionConverter
{
    internal static UserPermission ToUserPermission(this UserPermissionDb userPermissionDb)
    {
        return new UserPermission(userPermissionDb.UserId, userPermissionDb.RoleId);
    }
}
