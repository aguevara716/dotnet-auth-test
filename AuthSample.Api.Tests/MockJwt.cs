using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace AuthSample.Api.Tests;

public static class MockJwt
{
    public const string USERNAME = "testuser";
    public const string AUDIENCE = "sample-audience";

    public static string Issuer { get; } = Guid.NewGuid().ToString();
    public static SecurityKey SecurityKey { get; }
    public static SigningCredentials SigningCredentials { get; }

    private static readonly JwtSecurityTokenHandler tokenHandler = new();
    private static readonly RandomNumberGenerator generator = RandomNumberGenerator.Create();
    private static readonly byte[] key = new byte[32];
    private static readonly Claim[] defaultClaims = new Claim[]
    {
        new("adusername", USERNAME)
    };

    static MockJwt()
    {
        generator.GetBytes(key);
        SecurityKey = new SymmetricSecurityKey(key)
        {
            KeyId = Guid.NewGuid().ToString()
        };
        SigningCredentials = new(SecurityKey, SecurityAlgorithms.HmacSha256);
    }

    public static string GenerateToken(string type, string value)
    {
        var claim = new Claim(type, value);
        var token = GenerateToken(new[] { claim });
        return token;
    }

    public static string GenerateToken(IEnumerable<Claim> claims)
        => GenerateToken(claims.ToArray());

    public static string GenerateToken(params Claim[] claims)
    {
        var jst = new JwtSecurityToken
        (
            Issuer,
            AUDIENCE,
            claims.Concat(defaultClaims),
            null,
            DateTime.UtcNow.AddMinutes(20),
            SigningCredentials
        );
        var token = tokenHandler.WriteToken(jst);
        return token;
    }
}
