using AuthSample.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace AuthSample.Api;

public sealed class Program
{
    private static void SetUpPolicy(string policyName, AuthorizationOptions options)
    {
        options.AddPolicy(policyName, policy =>
        {
            _ = policy
                .RequireAuthenticatedUser()
                .RequireClaim("groups", AuthPolicy.POLICIES_TO_GROUPS[policyName]);
        });
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        _ = builder.Services.AddControllers();

        _ = builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        
        _ = builder.Services
            .AddAuthorization(options =>
            {
                SetUpPolicy(AuthPolicy.VIEWER_POLICY, options);
                SetUpPolicy(AuthPolicy.CUSTOMER_VIEWER_POLICY, options);
                SetUpPolicy(AuthPolicy.PRODUCT_VIEWER_POLICY, options);
                SetUpPolicy(AuthPolicy.MODIFIER_POLICY, options);
                SetUpPolicy(AuthPolicy.CUSTOMER_MODIFIER_POLICY, options);
                SetUpPolicy(AuthPolicy.PRODUCT_MODIFIER_POLICY, options);
                SetUpPolicy(AuthPolicy.ADMIN_POLICY, options);

                options.AddPolicy(AuthPolicy.NO_RESTRICTIONS_POLICY, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            })
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                #if DEBUG
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                        context.Token = token;
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication Failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token Validated Successfully");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine("Authentication Challenged: " + context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                };
                #endif
            });

        _ = builder.Services
            .AddSingleton<AuthorizationMiddlewareResultHandler>()
            .AddSingleton<IAuthorizationMiddlewareResultHandler, DebugAuthorizationMiddleware>();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            _ = app
                .UseSwagger()
                .UseSwaggerUI();
        }
        _ = app
            .UseHttpsRedirection()
            .UseAuthorization();
        
        _ = app.MapControllers();
        app.Run();
    }
}
