using System.Reflection;
namespace CleanArchitecture.Domain;

public static class DomainAssembly
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}