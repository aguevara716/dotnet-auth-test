using System.Net.Http.Headers;

namespace AuthSample.Api.Tests.Extensions;

public static class HttpClientExtensions
{
    public static HttpClient ClearAuthentication(this HttpClient client)
    {
        client.DefaultRequestHeaders.Clear();
        return client;
    }

    public static HttpClient AuthenticateWithGroupClaim(this HttpClient client, string group)
    {
        var token = MockJwt.GenerateToken("groups", group);
        return AuthenticateWithToken(client, token);
    }

    public static HttpClient AuthenticateWithoutClaims(this HttpClient client)
    {
        var token = MockJwt.GenerateToken();
        return AuthenticateWithToken(client, token);
    }

    private static HttpClient AuthenticateWithToken(this HttpClient client, string token)
    {
        _ = client.ClearAuthentication();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}
