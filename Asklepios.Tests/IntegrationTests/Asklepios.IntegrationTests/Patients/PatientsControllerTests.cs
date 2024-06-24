using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Patients;
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
    public async Task GetPatientById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = Guid.NewGuid();
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/patients-module/Patients/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
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
    
    [Fact]
    public async Task GetAllPatients_ShouldReturn_Ok_Status_And_Patients()
    {
        // Arrange
        const int pageIndex = 1;
        const int pageSize = 10;

        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Department 1", NumberOfBeds = 50, ActualNumberOfPatients = 20 };
        var room = new Room {RoomId = Guid.NewGuid(), RoomNumber = 115, RoomType = "Normal", NumberOfBeds = 4, DepartmentId = department.DepartmentId};
            
        var patients = new List<Patient>
        {
            new Patient {
                PatientId = Guid.NewGuid(),
                PatientName = "Test",
                PatientSurname = "Test",
                PeselNumber = "01300406342",
                InitialDiagnosis = "Test",
                IsDischarged = false,
                Treatment = "test",
                DepartmentId = department.DepartmentId,
                RoomId = room.RoomId }
        };

        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Rooms.AddAsync(room);
        await _testDatabase.DbContext.Patients.AddRangeAsync(patients);
        await _testDatabase.DbContext.SaveChangesAsync();

        var nurse = new User(Guid.NewGuid(), "katarzynawacnik@test.com", "password", Role.Nurse(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(nurse);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(nurse.UserId, nurse.Role);
    
        var response = await Client.GetAsync($"/patients-module/Patients?pageIndex={pageIndex}&pageSize={pageSize}");
        response.EnsureSuccessStatusCode();

        var departmentDtos = await response.Content.ReadFromJsonAsync<IReadOnlyList<PatientListDto>>();

        // Assert
        departmentDtos.ShouldNotBeNull();
        departmentDtos.Count.ShouldBe(patients.Count);
    }
    
    /*[Fact]
    public async Task DeletePatient_ShouldReturn_NoContent()
    {
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Department 1", NumberOfBeds = 50, ActualNumberOfPatients = 20 };
        var room = new Room {RoomId = Guid.NewGuid(), RoomNumber = 115, RoomType = "Normal", NumberOfBeds = 4, DepartmentId = department.DepartmentId};
        
        var patientId = Guid.NewGuid();
        
        var patient = new Patient
        {
            PatientId = patientId,
            PatientName = "Test",
            PatientSurname = "Test",
            PeselNumber = "01300406242",
            InitialDiagnosis = "Test",
            IsDischarged = false,
            Treatment = "test",
            DepartmentId = department.DepartmentId,
            RoomId = room.RoomId
        };
        
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Rooms.AddAsync(room);
        await _testDatabase.DbContext.Patients.AddAsync(patient);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        var nurse = new User(Guid.NewGuid(), "nurse@test.com", "password", Role.Nurse(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(nurse);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        Authorize(nurse.UserId, nurse.Role);

        // Act
        var response = await Client.DeleteAsync($"/patients-module/Patients/{patientId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        
        var deletedPatient = await _testDatabase.DbContext.Patients.FindAsync(patientId);
        deletedPatient.ShouldBeNull();
    }*/
    
}