using System.Reflection;

namespace AuthSample.Api.Security;

public static class UserGroup
{
    /// <summary>
    /// Has Administrator, Create, Read, Update, and Delete roles.
    /// </summary>
    public const string ADMINISTRATOR = "Administrator";

    /// <summary>
    /// Has Read role.
    /// </summary>
    public const string VIEWER = "Viewer";

    public const string CUSTOMER_VIEWER = "Customer.Viewer";

    public const string PRODUCT_VIEWER = "Product.Viewer";

    /// <summary>
    /// Has Create, Read, Update, and Delete roles.
    /// </summary>
    public const string MODIFIER = "Modifier";

    public const string CUSTOMER_MODIFIER = "Customer.Modifier";

    public const string PRODUCT_MODIFIER = "Product.Modifier";

    public static string[] GetAll()
    {
        var properties = typeof(UserGroup)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(pi => pi.GetValue(null)!.ToString()!)
            .ToArray();
        return properties;
    }
}
