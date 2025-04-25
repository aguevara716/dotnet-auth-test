using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace AuthSample.Api.Security;

public sealed class DebugAuthorizationMiddleware : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler;

    public DebugAuthorizationMiddleware(AuthorizationMiddlewareResultHandler defaultHandler)
    {
        _defaultHandler = defaultHandler;
    }

    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}
