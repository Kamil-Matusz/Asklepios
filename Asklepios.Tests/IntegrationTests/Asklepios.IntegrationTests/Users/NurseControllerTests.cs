using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
using Shouldly;

namespace Asklepios.IntegrationTests.Users;

[Collection("nurses")]
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
    public async Task GetAllNurses_ShouldReturn_Ok_WhenUserIsAdminOrITEmployee()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        Authorize(admin.UserId, admin.Role);

        const int pageIndex = 1;
        const int pageSize = 10;

        // Act
        var response = await Client.GetAsync($"/users-module/Nurse?pageIndex={pageIndex}&pageSize={pageSize}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task CreateNurse_ShouldReturn_Created_Status()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Nurse(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.SaveChangesAsync();

        var nurseDto = new Nurse
        {
            NurseId = Guid.NewGuid(),
            Name = "Test",
            Surname = "Test",
            UserId = user.UserId,
            DepartmentId = department.DepartmentId
        };

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PostAsJsonAsync("/users-module/Nurse", nurseDto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
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
    
    [Fact]
    public async Task GetNurseById_ShouldReturn_Ok_When_Nurse_Exists()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Nurse(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var nurse = new Nurse { NurseId = Guid.NewGuid(), Name = "Test", Surname = "Test", UserId = user.UserId, DepartmentId = department.DepartmentId };
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Nurses.AddAsync(nurse);
        await _testDatabase.DbContext.SaveChangesAsync();

        var admin = new User(Guid.NewGuid(), "admin1@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.GetAsync($"/users-module/Nurse/{nurse.NurseId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var nurseDto = await response.Content.ReadFromJsonAsync<NurseDto>();
        nurseDto.ShouldNotBeNull();
        nurseDto.NurseId.ShouldBe(nurse.NurseId);
    }
    
    [Fact]
    public async Task DeleteNurse_ShouldReturn_NoContent_When_Nurse_Deleted_Successfully()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Nurse(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var nurse = new Nurse { NurseId = Guid.NewGuid(), Name = "Test", Surname = "Test", UserId = user.UserId, DepartmentId = department.DepartmentId };
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Nurses.AddAsync(nurse);
        await _testDatabase.DbContext.SaveChangesAsync();

        var admin = new User(Guid.NewGuid(), "admin3@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.DeleteAsync($"/users-module/Nurse/{nurse.NurseId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task DeleteNurse_ShouldReturn_BadRequest_When_Nurse_Does_Not_Exist()
    {
        // Arrange
        var nonExistentNurseId = Guid.NewGuid();

        var admin = new User(Guid.NewGuid(), "admin4@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.DeleteAsync($"/users-module/Nurse/{nonExistentNurseId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UpdateNurse_ShouldReturn_NoContent_When_Nurse_Updated_Successfully()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Nurse(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var nurse = new Nurse { NurseId = Guid.NewGuid(), Name = "Test", Surname = "Test", UserId = user.UserId, DepartmentId = department.DepartmentId };
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Nurses.AddAsync(nurse);
        await _testDatabase.DbContext.SaveChangesAsync();

        var admin = new User(Guid.NewGuid(), "admin2@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        var updatedNurseDto = new NurseDto { NurseId = nurse.NurseId, Name = "Test", Surname = "Test", UserId = user.UserId, DepartmentId = department.DepartmentId };

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PutAsJsonAsync($"/users-module/Nurse/{nurse.NurseId}", updatedNurseDto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}