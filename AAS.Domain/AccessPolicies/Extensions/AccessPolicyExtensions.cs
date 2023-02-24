using AAS.Domain.AccessPolicies.Utils;

namespace AAS.Domain.AccessPolicies.Extensions;

public static class AccessPolicyExtensions
{
    public static Int32 Key(this AccessPolicy accessPolicy) => (Int32)accessPolicy;

    public static Int32[] Keys(this AccessPolicy[] accessPolicies) => accessPolicies.Select(Key).ToArray();

    public static AccessPolicyDetails ToDetails(this AccessPolicy accessPolicy)
    {
        return new AccessPolicyDetails(accessPolicy);
    }

    public static AccessPolicyDetails[] ToDetails(this AccessPolicy[] accessPolicies)
    {
        return accessPolicies.Select(ToDetails).ToArray();
    }
}
