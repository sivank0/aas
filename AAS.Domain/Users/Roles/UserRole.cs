using AAS.Domain.AccessPolicies;
using AAS.Tools.Types.IDs;

namespace AAS.Domain.Users.Roles;

public class UserRole
{
    public ID Id { get; }
    public String Name { get; }
    public AccessPolicy[] AccessPolicies { get; }

    public UserRole(ID id, String name, AccessPolicy[] accessPolicies)
    {
        Id = id;
        Name = name;
        AccessPolicies = accessPolicies;
    }
}
