using AAS.Domain.Users.Roles;
using AAS.Services.Users.Models;

namespace AAS.Services.Users.Converters;

internal static class UserRoleConverter
{
    public static UserRole ToUserRole(this UserRoleDb db)
    {
        return new UserRole(db.Id, db.Name, db.AccessPolicies);
    }
}
