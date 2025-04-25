using System.Net;
using AuthSample.Api.Security;
using AuthSample.Api.Tests.Extensions;
using FluentAssertions;

namespace AuthSample.Api.Tests;

public class SampleControllerTests : IClassFixture<AuthSampleWebAppFactory>
{
    private readonly AuthSampleWebAppFactory _factory;

    public SampleControllerTests(AuthSampleWebAppFactory factory)
    {
        _factory = factory;
    }

    // GET: /sample/anonymous | AllowAnonymous
    [Fact]
    public async Task AnonymousAction_Should_NotRequireLogin()
    {
        // setup
        _ = _factory.HttpClient.ClearAuthentication();

        // execute
        var response = await _factory.HttpClient.GetAsync("/sample/anonymous");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // GET: /sample/norestrictions | Authorize(Policy = AuthPolicy.NO_RESTRICTIONS_POLICY)
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task NoRestrictionsAction_Should_RequireLogin(bool isLoggedIn)
    {
        // setup
        if (isLoggedIn)
            _ = _factory.HttpClient.AuthenticateWithoutClaims();
        else
            _ = _factory.HttpClient.ClearAuthentication();

        // execute
        var response = await _factory.HttpClient.GetAsync("/sample/norestrictions");
        
        // assert
        var expectedStatusCode = isLoggedIn ? HttpStatusCode.OK : HttpStatusCode.Unauthorized;
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    // GET: /sample/admin | Authorize(Policy = AuthPolicy.ADMIN_POLICY)
    [Theory]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task AdminAction_Should_RequireAdminGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.GetAsync("/sample/admin");

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    // GET: /sample | Authorize(Policy = AuthPolicy.VIEWER_POLICY)
    [Theory]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.OK)]
    public async Task GetAction_Should_RequireViewerGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.GetAsync("/sample");

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Fact]
    public async Task GetAction_Should_FailIfGroupIsNotSet()
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithoutClaims();

        // execute
        var response = await _factory.HttpClient.GetAsync("/sample");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetAction_Should_RequireAuthentication()
    {
        // setup
        _ = _factory.HttpClient.ClearAuthentication();

        // execute
        var response = await _factory.HttpClient.GetAsync("/sample");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // POST: /sample | Authorize(Policy = AuthPolicy.MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task PostAction_Should_RequireModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.PostAsync("/sample", null);

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Fact]
    public async Task PostAction_Should_FailIfGroupIsNotSet()
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithoutClaims();

        // execute
        var response = await _factory.HttpClient.PostAsync("/sample", null);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task PostAction_Should_RequireAuthentication()
    {
        // setup
        _ = _factory.HttpClient.ClearAuthentication();

        // execute
        var response = await _factory.HttpClient.PostAsync("/sample", null);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // PUT: /sample | Authorize(Policy = AuthPolicy.MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task PutAction_Should_RequireModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.PutAsync("/sample", null);

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Fact]
    public async Task PutAction_Should_FailIfGroupIsNotSet()
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithoutClaims();
        
        // execute
        var response = await _factory.HttpClient.PutAsync("/sample", null);
        
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task PutAction_Should_RequireAuthentication()
    {
        // setup
        _ = _factory.HttpClient.ClearAuthentication();

        // execute
        var response = await _factory.HttpClient.PutAsync("/sample", null);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // DELETE: /sample | Authorize(Policy = AuthPolicy.MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task DeleteAction_Should_RequireModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.DeleteAsync("/sample");

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Fact]
    public async Task DeleteAction_Should_FailIfGroupIsNotSet()
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithoutClaims();

        // execute
        var response = await _factory.HttpClient.DeleteAsync("/sample");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteAction_Should_RequireAuthentication()
    {
        // setup
        _ = _factory.HttpClient.ClearAuthentication();

        // execute
        var response = await _factory.HttpClient.DeleteAsync("/sample");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
