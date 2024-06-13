using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Shouldly;

namespace Asklepios.IntegrationTests.Patients;

[Collection("patients")]
public class PatientsControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public PatientsControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task Create_Patient_Should_Return_Ok_Status()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";

        var doctor = new User(Guid.NewGuid(), "patientDoctor1@test.com", passwordManager.Secure(password), Role.Doctor(), true,
            DateTime.Now);

        var patientDto = new PatientDto
        {
            PatientId = Guid.NewGuid(),
            PatientName = "Test",
            PatientSurname = "Test",
            PeselNumber = "01300406342",
            InitialDiagnosis = "Test",
            IsDischarged = false,
            Treatment = "test",
            DepartmentId = Guid.NewGuid(),
            RoomId = Guid.NewGuid()
        };

        await _testDatabase.DbContext.Users.AddAsync(doctor);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(doctor.UserId, doctor.Role);
        var response = await Client.PostAsJsonAsync("patients-module/Patients", patientDto);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
}