namespace AuthSample.Api.Security;

public static class AuthPolicy
{
    public const string ADMIN_POLICY = "AdminPolicy";
    
    public const string VIEWER_POLICY = "ViewerPolicy";

    public const string CUSTOMER_VIEWER_POLICY = "Customer.ViewerPolicy";

    public const string PRODUCT_VIEWER_POLICY = "Product.ViewerPolicy";
    
    public const string MODIFIER_POLICY = "ModifierPolicy";

    public const string CUSTOMER_MODIFIER_POLICY = "Customer.ModifierPolicy";

    public const string PRODUCT_MODIFIER_POLICY = "Product.ModifierPolicy";
    
    /// <summary>
    /// Policy for actions that don't require any specific group, but do require authentication
    /// </summary>
    public const string NO_RESTRICTIONS_POLICY = "NoRestrictionsPolicy";

    /// <summary>
    /// Key: Policy name
    /// <para>
    /// Value: Array of user groups that are granted via the policy. If a user belongs to at least
    /// one of the groups in the array, they are granted access to the resource via the policy.
    /// </para>
    /// </summary>
    public static readonly IReadOnlyDictionary<string, string[]> POLICIES_TO_GROUPS = new Dictionary<string, string[]>()
    {
        {
            ADMIN_POLICY,
            new[]
            {
                UserGroup.ADMINISTRATOR
            }
        },
        {
            VIEWER_POLICY,
            new[]
            {
                UserGroup.VIEWER,
                UserGroup.MODIFIER,
                UserGroup.ADMINISTRATOR
            }
        },
        {
            CUSTOMER_VIEWER_POLICY,
            new[]
            {
                UserGroup.CUSTOMER_VIEWER,
                UserGroup.CUSTOMER_MODIFIER,
                UserGroup.ADMINISTRATOR
            }
        },
        {
            PRODUCT_VIEWER_POLICY,
            new[]
            {
                UserGroup.PRODUCT_VIEWER,
                UserGroup.PRODUCT_MODIFIER,
                UserGroup.ADMINISTRATOR
            }
        },
        {
            MODIFIER_POLICY,
            new[]
            {
                UserGroup.MODIFIER,
                UserGroup.ADMINISTRATOR
            }
        },
        {
            CUSTOMER_MODIFIER_POLICY,
            new[]
            {
                UserGroup.CUSTOMER_MODIFIER,
                UserGroup.ADMINISTRATOR
            }
        },
        {
            PRODUCT_MODIFIER_POLICY,
            new[]
            {
                UserGroup.PRODUCT_MODIFIER,
                UserGroup.ADMINISTRATOR
            }
        }
    };
}
