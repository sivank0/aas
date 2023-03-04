using AAS.Domain.AccessPolicies.Utils;
using AAS.Tools.Extensions;

namespace AAS.Domain.AccessPolicies.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class AccessPolicyBlockAttribute : Attribute
{
    public Int32 Key { get; }
    public String DisplayName { get; }

    public AccessPolicyBlockAttribute(AccessPolicyBlock accessPolicyBlock)
    {
        Key = (Int32)accessPolicyBlock;
        DisplayName = accessPolicyBlock.GetDisplayName();
    }
}
