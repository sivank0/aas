#region

using AAS.Domain.AccessPolicies;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Domain.Users.Roles;

public class UserRole
{
    public ID Id { get; }
    public string Name { get; }
    public AccessPolicy[] AccessPolicies { get; }

    public UserRole(ID id, string name, AccessPolicy[] accessPolicies)
    {
        Id = id;
        Name = name;
        AccessPolicies = accessPolicies;
    }
}