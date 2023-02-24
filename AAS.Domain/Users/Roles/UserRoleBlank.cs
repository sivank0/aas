using AAS.Domain.AccessPolicies;
using AAS.Tools.Types.IDs;

namespace AAS.Domain.Users.Roles;

public class UserRoleBlank
{
    public ID? Id { get; set; }
    public String? Name { get; set; }
    public AccessPolicy[] AccessPolicies { get; set; } = Array.Empty<AccessPolicy>();
}
