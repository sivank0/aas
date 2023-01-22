namespace AAS.Domain.Users.Permissions;

public class UserPermission
{
    public Guid UserId { get; }
    public Guid RoleId { get; }

    public UserPermission(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}
