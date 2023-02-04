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
    public Boolean IsEmployee { get; }

    public SystemUser(User user, UserAccess currentAccess, Boolean isEmployee)
    {
        Id = user.Id;
        Email = user.Email;
        FullName = user.FullName;
        Access = currentAccess;
        IsEmployee = isEmployee;
    }

    public Boolean HasAccess(AccessPolicy accessPolicy) => Access.AccessPolicies.Contains(accessPolicy);
}