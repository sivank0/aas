using AAS.Domain.AccessPolicies;
using AAS.Domain.Users.UserAccesses;
using AAS.Tools.Types.IDs;

namespace AAS.Domain.Users.SystemUsers;

public class SystemUser
{
    public ID Id { get; }
    public String Email { get; }
    public String FullName { get; }
    public UserAccess Access { get; }

    public SystemUser(User user, UserAccess currentAccess)
    {
        Id = user.Id;
        Email = user.Email;
        FullName = user.FullName;
        Access = currentAccess;
    }

    public Boolean HasAccess(AccessPolicy accessPolicy) => Access.AccessPolicies.Contains(accessPolicy);
}