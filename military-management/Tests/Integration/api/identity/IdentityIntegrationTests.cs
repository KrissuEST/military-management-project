using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Public.DTO.v1.Identity;
using Xunit.Abstractions;

namespace Tests.Integration.api.identity;

public class IdentityIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;


    public IdentityIntegrationTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "POST - register new user")]
    public async Task RegisterNewUserTest()
    {
        // Arrange
        var URL =  "/api/v1/identity/account/register";
        
        var registerData = new
        {
            Email = "test@test.ee",
            Password = "Foo.bar1",
            Firstname = "Test first",
            Lastname = "Test last",
        };
        
        var data = JsonContent.Create(registerData);

        // Act
        var response = await _client.PostAsync(URL, data);

        // We need to get the data
        var responseContent = await response.Content.ReadAsStringAsync();
        
        // Assert
        Assert.Equal(true, response.IsSuccessStatusCode);

        var jwtResponse = System.Text.Json.JsonSerializer.Deserialize<JWTResponse>(responseContent);
        
        Assert.NotNull(jwtResponse);
        // Now let's access some data with it.
    }
    
    [Fact(DisplayName = "POST - login user")]
    public async Task LoginUserTest()
    {
        // Arrange
        // Act
        // Assert
    }
    
    [Fact(DisplayName = "POST - login user failed")]
    public async Task LoginUserTest2()
    {
        // Arrange
        // Act
        // Assert
    }
    
    [Fact(DisplayName = "POST - JWT expired")]
    public async Task JWTExpired()
    {
        // Arrange
        // Act
        // Assert
    }
    
    [Fact(DisplayName = "POST - JWT renewal")]
    public async Task JWTRenewal()
    {
        // Arrange
        // Act
        // Assert
    }
}