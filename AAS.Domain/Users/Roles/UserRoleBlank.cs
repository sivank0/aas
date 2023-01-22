using AAS.Domain.AccessPolicies;

namespace AAS.Domain.Users.Roles;

public class UserRoleBlank
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public AccessPolicy[] AccessPolicies { get; set; } = Array.Empty<AccessPolicy>();
}
