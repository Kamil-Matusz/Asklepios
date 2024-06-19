using System.Net;
using Shouldly;

namespace Asklepios.IntegrationTests.Departments;

[Collection("rooms")]
public class RoomControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public RoomControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task GetRoomById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = Guid.NewGuid();
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/departments-module/Rooms/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}