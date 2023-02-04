using AAS.Domain.AccessPolicies;

namespace AAS.Domain.Users.UserAccesses;

public class UserAccess
{
    public AccessPolicy[] AccessPolicies { get; }
    public Boolean HasFullAccess { get; }

    public UserAccess(AccessPolicy[] accessPolicies, Boolean hasFullAccess = false)
    {
        AccessPolicies = accessPolicies;
        HasFullAccess = hasFullAccess;
    }
}