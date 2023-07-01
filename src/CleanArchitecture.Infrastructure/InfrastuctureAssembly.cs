using System.Reflection;

namespace CleanArchitecture.Infrastructure;

public static class InfrastructureAssembly
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
