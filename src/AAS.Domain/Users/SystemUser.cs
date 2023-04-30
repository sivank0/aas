#region

using AAS.Domain.AccessPolicies;
using AAS.Domain.Users.UserAccesses;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Domain.Users.SystemUsers;

public class SystemUser
{
    public ID Id { get; }
    public string Email { get; }
    public string FullName { get; }
    public UserAccess Access { get; }

    public SystemUser(User user, UserAccess currentAccess)
    {
        Id = user.Id;
        Email = user.Email;
        FullName = user.FullName;
        Access = currentAccess;
    }

    public bool HasAccess(AccessPolicy accessPolicy)
    {
        return Access.AccessPolicies.Contains(accessPolicy);
    }
}