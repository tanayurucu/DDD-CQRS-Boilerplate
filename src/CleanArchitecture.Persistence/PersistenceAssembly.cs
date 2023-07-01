using System.Reflection;

namespace CleanArchitecture.Persistence;

public static class PersistenceAssembly
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
