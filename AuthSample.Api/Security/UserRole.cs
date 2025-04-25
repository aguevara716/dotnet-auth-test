using System.Reflection;

namespace AuthSample.Api.Security;

public class UserRole
{
    public const string ADMINISTRATOR = "Administrator";

    public const string CREATE = "Create";

    public const string READ = "Read";

    public const string UPDATE = "Update";

    public const string DELETE = "Delete";

    public static string[] GetAll()
    {
        var properties = typeof(UserRole)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(pi => pi.GetValue(null)!.ToString()!)
            .ToArray();
        return properties;
    }
}
