#region

using System.Reflection;

#endregion

namespace AAS.Domain;

public static class DomainAssembly
{
    public static Assembly Itself => typeof(DomainAssembly).Assembly;
}