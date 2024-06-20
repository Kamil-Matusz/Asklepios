using System.Net;
using Shouldly;

namespace Asklepios.IntegrationTests.Users;

[Collection("users")]
public class NurseControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public NurseControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task GetNurseById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = Guid.NewGuid();
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/users-module/Nurse/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}