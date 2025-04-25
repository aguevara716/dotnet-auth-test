using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AuthSample.Api.Tests;

public sealed class AuthSampleWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public HttpClient HttpClient { get; private set; } = default!;

    public Task InitializeAsync()
    {
        HttpClient = CreateClient();
        return Task.CompletedTask;
    }

    Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _ = builder.ConfigureServices(services =>
        {
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var config = new OpenIdConnectConfiguration
                {
                    Issuer = MockJwt.Issuer,
                };
                config.SigningKeys.Add(MockJwt.SecurityKey);
                options.Configuration = config;
                options.Audience = MockJwt.AUDIENCE;
            });
        });
    }
}
