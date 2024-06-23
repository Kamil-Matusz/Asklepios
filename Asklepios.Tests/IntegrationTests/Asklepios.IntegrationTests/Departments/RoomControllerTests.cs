using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Departments;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
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
    
    [Fact]
    public async Task GetAllRooms_ShouldReturn_Unauthorized_WhenUserIsNotAuthenticated()
    {
        // Arrange
        const int pageIndex = 1;
        const int pageSize = 10;

        // Act
        var response = await Client.GetAsync($"/departments-module/Rooms?pageIndex={pageIndex}&pageSize={pageSize}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}