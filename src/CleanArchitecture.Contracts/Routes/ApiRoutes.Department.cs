namespace CleanArchitecture.Contracts.Routes;

public static partial class ApiRoutes
{
    public static class Departments
    {
        public const string Get = "departments";
        public const string GetById = "departments/{departmentId:guid}";
        public const string Create = "departments";
        public const string Update = "departments/{departmentId:guid}";
        public const string Delete = "departments/{departmentId:guid}";
    }
}