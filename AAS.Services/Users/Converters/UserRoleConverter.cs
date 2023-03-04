using AAS.Domain.Users.Roles;
using AAS.Services.Users.Models;

namespace AAS.Services.Users.Converters;

internal static class UserRoleConverter
{
    internal static UserRole ToUserRole(this UserRoleDb db)
    {
        return new UserRole(db.Id, db.Name, db.AccessPolicies);
    }

    internal static UserRole[] ToUserRoles(this UserRoleDb[] dbs)
    {
        return dbs.Select(ToUserRole).ToArray();
    }
}
