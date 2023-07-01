namespace CleanArchitecture.Infrastructure.Email.Constants;

public static class MailTemplates
{
    private const string BasePath = "../CleanArchitecture.Infrastructure/Email/Templates";

    public static string Welcome => File.ReadAllText(Path.Combine(BasePath, "welcome.mjml"));
}