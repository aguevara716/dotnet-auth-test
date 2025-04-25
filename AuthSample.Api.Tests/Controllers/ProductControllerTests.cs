using System.Net;
using AuthSample.Api.Security;
using AuthSample.Api.Tests.Extensions;
using FluentAssertions;

namespace AuthSample.Api.Tests.Controllers;

public sealed class ProductControllerTests : IClassFixture<AuthSampleWebAppFactory>
{
    private readonly AuthSampleWebAppFactory _factory;

    public ProductControllerTests(AuthSampleWebAppFactory factory)
    {
        _factory = factory;
    }

    // GET /product | Authorize(Policy = AuthPolicy.PRODUCT_VIEWER_POLICY)
    [Theory]
    [InlineData(UserGroup.PRODUCT_VIEWER, HttpStatusCode.OK)]
    [InlineData(UserGroup.PRODUCT_MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.CUSTOMER_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.CUSTOMER_MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task GetAction_Should_RequireProductViewerGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.GetAsync("/product");

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    // POST /product | Authorize(Policy = AuthPolicy.PRODUCT_MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.PRODUCT_MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.PRODUCT_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.CUSTOMER_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.CUSTOMER_MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task PostAction_Should_RequireProductModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.PostAsync("/product", null);

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    // PUT /product | Authorize(Policy = AuthPolicy.PRODUCT_MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.PRODUCT_MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.PRODUCT_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.CUSTOMER_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.CUSTOMER_MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task PutAction_Should_RequireProductModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.PutAsync("/product", null);

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    // DELETE /product | Authorize(Policy = AuthPolicy.PRODUCT_MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.PRODUCT_MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.PRODUCT_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.CUSTOMER_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.CUSTOMER_MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task DeleteAction_Should_RequireProductModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.DeleteAsync("/product");

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }
}
