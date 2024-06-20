using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Entities.Examinations;
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
    public async Task GetExaminationById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = 258;
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/examinations-module/Examinations/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
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
    
    [Fact]
    public async Task GetAllExaminations_ShouldReturn_Ok_Status_And_Examinations()
    {
        // Arrange
        const int pageIndex = 1;
        const int pageSize = 10;

        var examinations = new List<Examination>
        {
            new Examination { ExaminationId = 1, ExamName = "Examination 1", ExamCategory = ExamCategory.Lab()},
            new Examination { ExaminationId = 2, ExamName = "Examination 2", ExamCategory = ExamCategory.Lab()},
        };

        await _testDatabase.DbContext.Examinations.AddRangeAsync(examinations);
        await _testDatabase.DbContext.SaveChangesAsync();

        var admin = new User(Guid.NewGuid(), "administrator@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
    
        var response = await Client.GetAsync($"/examinations-module/Examinations?pageIndex={pageIndex}&pageSize={pageSize}");
        response.EnsureSuccessStatusCode();

        var examinationDtos = await response.Content.ReadFromJsonAsync<IReadOnlyList<ExaminationDto>>();

        // Assert
        examinationDtos.ShouldNotBeNull();
        examinationDtos.Count.ShouldBe(examinations.Count);
    }
    
    [Fact]
    public async Task GetExamination_ShouldReturn_Ok_Status_And_Examination()
    {
        // Arrange
        var examination = new Examination { ExaminationId = 3, ExamName = "Examination 3", ExamCategory = ExamCategory.Lab() };
        await _testDatabase.DbContext.Examinations.AddAsync(examination);
        await _testDatabase.DbContext.SaveChangesAsync();

        var admin = new User(Guid.NewGuid(), "administrator2@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.GetAsync($"/examinations-module/Examinations/{examination.ExaminationId}");
        response.EnsureSuccessStatusCode();

        var examinationDto = await response.Content.ReadFromJsonAsync<ExaminationDto>();

        // Assert
        examinationDto.ShouldNotBeNull();
    }

}