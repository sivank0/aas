#region

using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Services.Users.Models;

public class UserPermissionDb
{
    public ID UserId { get; set; }
    public ID RoleId { get; set; }

    public UserPermissionDb(ID userId, ID roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}