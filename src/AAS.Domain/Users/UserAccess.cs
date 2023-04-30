#region

using AAS.Domain.AccessPolicies;

#endregion

namespace AAS.Domain.Users.UserAccesses;

public class UserAccess
{
    public AccessPolicy[] AccessPolicies { get; }

    public UserAccess(AccessPolicy[] accessPolicies)
    {
        AccessPolicies = accessPolicies;
    }
}