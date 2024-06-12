using System.Net;
using System.Net.Http.Json;
using Asklepios.Application.Commands.Users;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Shouldly;

namespace Asklepios.IntegrationTests.UsersControllers;

public class UsersControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }

    [Fact]
    public async Task SignUpUser_ShouldReturn_Ok_Status()
    {
        // Arrange
        var command = new SignUp(Guid.Empty, "testUser@test.com", "password", "Doctor", true);
        
        // Act
        var response = await Client.PostAsJsonAsync("users-module/Users/signUp", command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task SignIn_ShouldReturn_OkStatus()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";

        var user = new User(Guid.NewGuid(), "admin@test.com", passwordManager.Secure(password), Role.Admin(), true,
            DateTime.Now);

        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        var command = new SignIn(user.Email, password);
        var response = await Client.PostAsJsonAsync("users-module/Users/signIn", command);
        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();

        // Assert
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetMyAccount_ShouldReturn_Ok_Status_And_User()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";
        
        var user = new User(Guid.NewGuid(), "administrator@test.com", passwordManager.Secure(password), Role.Admin(), true,
            DateTime.Now);
        
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(user.UserId, user.Role);
        var userId = user.UserId;
        
        var accountDto = await Client.GetFromJsonAsync<AccountDto>("users-module/Users/myAccount");
        
        // Assert
        accountDto.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task GetUserAccount_ShouldReturn_Ok_Status_And_User()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";
        
        var admin = new User(Guid.NewGuid(), "administrator2@test.com", passwordManager.Secure(password), Role.Admin(), true,
            DateTime.Now);
        
        var user = new User(Guid.NewGuid(), "user2@test.com", passwordManager.Secure(password), Role.IT_Employee(), true,
            DateTime.Now);
        
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var userId = user.UserId;
        
        var accountDto = await Client.GetFromJsonAsync<AccountDto>($"users-module/Users/{userId}");
        
        // Assert
        accountDto.ShouldNotBeNull();
        accountDto.UserId.ShouldBe(user.UserId);
    }

    [Fact]
    public async Task GenerateUserAccount_ShouldReturn_Ok_Status_And_User()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";
        
        var admin = new User(Guid.NewGuid(), "administrator3@test.com", passwordManager.Secure(password), Role.Admin(), true,
            DateTime.Now);
        
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        // Act
        Authorize(admin.UserId, admin.Role);
        var command = new GenerateUserAccount(Guid.Empty, "user3@test.com", Role.IT_Employee(), true);
        var response = await Client.PostAsJsonAsync("users-module/Users/generateUserAccount", command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

}