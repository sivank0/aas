﻿#region

using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Domain.Users.Permissions;

public class UserPermission
{
    public ID UserId { get; }
    public ID RoleId { get; }

    public UserPermission(ID userId, ID roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}