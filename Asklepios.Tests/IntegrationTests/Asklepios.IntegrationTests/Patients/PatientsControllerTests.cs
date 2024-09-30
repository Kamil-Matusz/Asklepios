using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
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
    public async Task GetAllPatients_ShouldReturn_Ok_WhenUserIsInGoodRole()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        Authorize(admin.UserId, admin.Role);

        const int pageIndex = 1;
        const int pageSize = 10;

        // Act
        var response = await Client.GetAsync($"/patients-module/Patients?pageIndex={pageIndex}&pageSize={pageSize}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
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
    public async Task CreatePatient_ShouldReturn_Created_WhenValidDataIsProvided()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var room = new Room { RoomId = Guid.NewGuid(), RoomNumber = 105, RoomType = "Ogólna", NumberOfBeds = 4, DepartmentId = department.DepartmentId};
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Doctor(), true, DateTime.Now);
        var doctor = new MedicalStaff { DoctorId = Guid.NewGuid(), Name = "Test", Surname = "Test", PrivatePhoneNumber = "+48123456789", HospitalPhoneNumber = "+48987654321", Specialization = "Test", MedicalLicenseNumber = "ABC123", UserId = user.UserId, DepartmentId = department.DepartmentId };
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Rooms.AddAsync(room);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.MedicalStaff.AddAsync(doctor);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        var newPatient = new PatientDto
        {
            PatientId = Guid.NewGuid(),
            PatientName = "Test",
            PatientSurname = "Test",
            PeselNumber = "01300407251",
            InitialDiagnosis = "Test",
            IsDischarged = false,
            Treatment = "Test",
            DepartmentId = department.DepartmentId,
            RoomId = room.RoomId,
            MedicalStaffId = doctor.DoctorId,
            AdmissionDate = DateOnly.FromDateTime(DateTime.Today),
            Address = "Test"
        };

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PostAsJsonAsync($"/patients-module/Patients", newPatient);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task GetPatientsList_ShouldReturn_Ok_WhenCalledByAuthorizedUser()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.GetAsync($"/patients-module/Patients/patientsList");
        var patientsList = await response.Content.ReadFromJsonAsync<IEnumerable<PatientAutocompleteDto>>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        patientsList.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task DeletePatient_ShouldReturn_NoContent_WhenPatientIsDeleted()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var room = new Room { RoomId = Guid.NewGuid(), RoomNumber = 105, RoomType = "Ogólna", NumberOfBeds = 4, DepartmentId = department.DepartmentId};
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Doctor(), true, DateTime.Now);
        var doctor = new MedicalStaff { DoctorId = Guid.NewGuid(), Name = "Test", Surname = "Test", PrivatePhoneNumber = "+48123456789", HospitalPhoneNumber = "+48987654321", Specialization = "Test", MedicalLicenseNumber = "ABC123", UserId = user.UserId, DepartmentId = department.DepartmentId };
        var patient = new Patient { PatientId = Guid.NewGuid(), PatientName = "Test", PatientSurname = "Test", PeselNumber = "01300407251", InitialDiagnosis = "Test", IsDischarged = false, Treatment = "Test", DepartmentId = department.DepartmentId, RoomId = room.RoomId, MedicalStaffId = doctor.DoctorId, AdmissionDate = DateOnly.FromDateTime(DateTime.Today), Address = "Test" };
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Rooms.AddAsync(room);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.MedicalStaff.AddAsync(doctor);
        await _testDatabase.DbContext.Patients.AddAsync(patient);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.DeleteAsync($"/patients-module/Patients/{patient.PatientId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task UpdatePatient_ShouldReturn_NoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var room = new Room { RoomId = Guid.NewGuid(), RoomNumber = 105, RoomType = "Ogólna", NumberOfBeds = 4, DepartmentId = department.DepartmentId};
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Doctor(), true, DateTime.Now);
        var doctor = new MedicalStaff { DoctorId = Guid.NewGuid(), Name = "Test", Surname = "Test", PrivatePhoneNumber = "+48123456789", HospitalPhoneNumber = "+48987654321", Specialization = "Test", MedicalLicenseNumber = "ABC123", UserId = user.UserId, DepartmentId = department.DepartmentId };
        var patient = new Patient { PatientId = Guid.NewGuid(), PatientName = "Test", PatientSurname = "Test", PeselNumber = "01300407251", InitialDiagnosis = "Test", IsDischarged = false, Treatment = "Test", DepartmentId = department.DepartmentId, RoomId = room.RoomId, MedicalStaffId = doctor.DoctorId, AdmissionDate = DateOnly.FromDateTime(DateTime.Today), Address = "Test" };
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.Rooms.AddAsync(room);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.MedicalStaff.AddAsync(doctor);
        await _testDatabase.DbContext.Patients.AddAsync(patient);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        var updatePatient = new PatientDto
        {
            PatientId = Guid.NewGuid(),
            PatientName = "Test",
            PatientSurname = "Test",
            PeselNumber = "01300407251",
            InitialDiagnosis = "Test",
            IsDischarged = false,
            Treatment = "Test",
            DepartmentId = department.DepartmentId,
            RoomId = room.RoomId,
            MedicalStaffId = doctor.DoctorId,
            AdmissionDate = DateOnly.FromDateTime(DateTime.Today),
            Address = "Test"
        };

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PutAsJsonAsync($"/patients-module/Patients/{patient.PatientId}", updatePatient);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        // Verify Update
       
    }
}