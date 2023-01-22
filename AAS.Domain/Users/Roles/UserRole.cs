using AAS.Domain.AccessPolicies;

namespace AAS.Domain.Users.Roles;

public class UserRole
{
    public Guid Id { get; }
    public string Name { get; }
    public AccessPolicy[] AccessPolicies { get; }

    public UserRole(Guid id, string name, AccessPolicy[] accessPolicies)
    {
        Id = id;
        Name = name;
        AccessPolicies = accessPolicies;
    }
}
