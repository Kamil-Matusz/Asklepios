using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Shouldly;

namespace Asklepios.IntegrationTests.Examinations;

[Collection("examination")]
public class ExaminationControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public ExaminationControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
       _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task Create_Examination_Should_Return_Ok_Status()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";

        var admin = new User(Guid.NewGuid(), "admin1@asklepios.com", passwordManager.Secure(password), Role.Admin(), true,
            DateTime.Now);

        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        var examDto = new ExaminationDto { ExamId = 1, ExamName = "Test Exam", ExamCategory = ExamCategory.Lab() };

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PostAsJsonAsync("examinations-module/Examinations", examDto);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
}