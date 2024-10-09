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
    public async Task CreateRoom_ShouldReturn_Created_Status()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "roomAdmin1@test.com", "password", Role.Admin(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.SaveChangesAsync();

        var roomDto = new Room { RoomId = Guid.NewGuid(), RoomNumber = 105, RoomType = "Ogólna", NumberOfBeds = 4, DepartmentId = department.DepartmentId};

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PostAsJsonAsync("/departments-module/Rooms", roomDto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
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
    public async Task GetRoomById_ShouldReturn_Ok_When_Room_Exists()
    {
        // Arrange
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var room = new Room { RoomId = Guid.NewGuid(), RoomNumber = 105, RoomType = "Ogólna", NumberOfBeds = 4, DepartmentId = department.DepartmentId};
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Rooms.AddAsync(room);
        await _testDatabase.DbContext.SaveChangesAsync();

        var admin = new User(Guid.NewGuid(), "roomAdmin2@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.GetAsync($"/departments-module/Rooms/{room.RoomId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var roomDto = await response.Content.ReadFromJsonAsync<RoomDetailsDto>();
        roomDto.ShouldNotBeNull();
        roomDto.RoomId.ShouldBe(room.RoomId);
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
    
    [Fact]
    public async Task DeleteRoom_ShouldReturn_BadRequest_When_Room_Does_Not_Exist()
    {
        // Arrange
        var nonExistentRoomId = Guid.NewGuid();

        var admin = new User(Guid.NewGuid(), "roomAdmin5@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.DeleteAsync($"/departments-module/Rooms/{nonExistentRoomId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task DeleteRoom_ShouldReturn_NoContent_When_Room_Deleted_Successfully()
    {
        // Arrange
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var room = new Room { RoomId = Guid.NewGuid(), RoomNumber = 105, RoomType = "Ogólna", NumberOfBeds = 4, DepartmentId = department.DepartmentId};
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Rooms.AddAsync(room);
        await _testDatabase.DbContext.SaveChangesAsync();

        var admin = new User(Guid.NewGuid(), "roomAdmin4@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.DeleteAsync($"/departments-module/Rooms/{room.RoomId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task UpdateRoom_ShouldReturn_NoContent_When_Room_Updated_Successfully()
    {
        // Arrange
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var room = new Room { RoomId = Guid.NewGuid(), RoomNumber = 105, RoomType = "Ogólna", NumberOfBeds = 4, DepartmentId = department.DepartmentId};
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Rooms.AddAsync(room);
        await _testDatabase.DbContext.SaveChangesAsync();

        var admin = new User(Guid.NewGuid(), "roomAdmin3@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        var updatedRoomDto = new Room { RoomId = Guid.NewGuid(), RoomNumber = 104, RoomType = "Ogólna", NumberOfBeds = 4, DepartmentId = department.DepartmentId};

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PutAsJsonAsync($"/departments-module/Rooms/{room.RoomId}", updatedRoomDto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}