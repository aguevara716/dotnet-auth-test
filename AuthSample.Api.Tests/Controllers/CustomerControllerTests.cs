using System.Net;
using AuthSample.Api.Security;
using AuthSample.Api.Tests.Extensions;
using FluentAssertions;

namespace AuthSample.Api.Tests.Controllers;

public sealed class CustomerControllerTests : IClassFixture<AuthSampleWebAppFactory>
{
    private readonly AuthSampleWebAppFactory _factory;

    public CustomerControllerTests(AuthSampleWebAppFactory factory)
    {
        _factory = factory;
    }

    // GET /customer | Authorize(Policy = AuthPolicy.CUSTOMER_VIEWER_POLICY)
    [Theory]
    [InlineData(UserGroup.CUSTOMER_VIEWER, HttpStatusCode.OK)]
    [InlineData(UserGroup.CUSTOMER_MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.PRODUCT_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.PRODUCT_MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task GetAction_Should_RequireCustomerViewerGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.GetAsync("/customer");

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    // POST /customer | Authorize(Policy = AuthPolicy.CUSTOMER_MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.CUSTOMER_MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.CUSTOMER_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.PRODUCT_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.PRODUCT_MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task PostAction_Should_RequireCustomerModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.PostAsync("/customer", null);

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    // PUT /customer | Authorize(Policy = AuthPolicy.CUSTOMER_MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.CUSTOMER_MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.CUSTOMER_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.PRODUCT_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.PRODUCT_MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task PutAction_Should_RequireCustomerModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.PutAsync("/customer", null);

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    // DELETE /customer | Authorize(Policy = AuthPolicy.CUSTOMER_MODIFIER_POLICY)
    [Theory]
    [InlineData(UserGroup.CUSTOMER_MODIFIER, HttpStatusCode.OK)]
    [InlineData(UserGroup.ADMINISTRATOR, HttpStatusCode.OK)]
    [InlineData(UserGroup.CUSTOMER_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.PRODUCT_VIEWER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.PRODUCT_MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.MODIFIER, HttpStatusCode.Forbidden)]
    [InlineData(UserGroup.VIEWER, HttpStatusCode.Forbidden)]
    public async Task DeleteAction_Should_RequireCustomerModifierGroup(string group, HttpStatusCode expectedStatusCode)
    {
        // setup
        _ = _factory.HttpClient.AuthenticateWithGroupClaim(group);

        // execute
        var response = await _factory.HttpClient.DeleteAsync("/customer");

        // assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }
}
