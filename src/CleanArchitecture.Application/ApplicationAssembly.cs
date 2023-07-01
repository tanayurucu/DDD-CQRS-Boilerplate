using System.Reflection;

namespace CleanArchitecture.Application;

public static class ApplicationAssembly
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}