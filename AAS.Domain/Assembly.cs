using System.Reflection;

namespace AAS.Domain;

public static class DomainAssembly
{
    public static Assembly Itself => typeof(DomainAssembly).Assembly;
}