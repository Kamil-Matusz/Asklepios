using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Departments;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Shouldly;

namespace Asklepios.IntegrationTests.Departments;

[Collection("departments")]
public class DepartmentControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public DepartmentControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task Create_Department_Should_Return_Ok_Status()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";

        var admin = new User(Guid.NewGuid(), "departmentAdmin1@test.com", passwordManager.Secure(password), Role.Admin(), true,
            DateTime.Now);

        var departmentDto = new DepartmentDto
        {
            DepartmentId = Guid.NewGuid(),
            DepartmentName = "Kardiologia",
            NumberOfBeds = 50,
            ActualNumberOfPatients = 20
        };

        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PostAsJsonAsync("departments-module/Departments", departmentDto);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task GetDepartmentById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = Guid.NewGuid();
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/departments-module/Departments/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}