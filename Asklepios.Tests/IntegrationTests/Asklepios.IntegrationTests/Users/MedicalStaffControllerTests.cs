using System.Net;
using System.Net.Http.Json;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
using Shouldly;

namespace Asklepios.IntegrationTests.Users;

[Collection("medicalStaff")]
public class MedicalStaffControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public MedicalStaffControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task GetAllDoctors_ShouldReturn_Ok_WhenUserIsAdminOrITEmployee()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        Authorize(admin.UserId, admin.Role);

        const int pageIndex = 1;
        const int pageSize = 10;

        // Act
        var response = await Client.GetAsync($"/users-module/MedicalStaff?pageIndex={pageIndex}&pageSize={pageSize}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetDoctorById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = Guid.NewGuid();
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/users-module/MedicalStaff/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetDoctor_ShouldReturn_NotFound_WhenDoctorDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        await _testDatabase.DbContext.Users.AddAsync(admin);
        Authorize(admin.UserId, admin.Role);

        // Act
        var response = await Client.GetAsync($"/users-module/MedicalStaff/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task GetDoctor_ShouldReturn_Ok_WhenDoctorExists()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Doctor(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var doctor = new MedicalStaff { DoctorId = Guid.NewGuid(), Name = "Test", Surname = "Test", PrivatePhoneNumber = "+48123456789", HospitalPhoneNumber = "+48987654321", Specialization = "Test", MedicalLicenseNumber = "ABC123", UserId = user.UserId, DepartmentId = department.DepartmentId };
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.MedicalStaff.AddAsync(doctor);
        await _testDatabase.DbContext.SaveChangesAsync();
        Authorize(admin.UserId, admin.Role);

        // Act
        var response = await Client.GetAsync($"/users-module/MedicalStaff/{doctor.DoctorId}");
        var result = await response.Content.ReadFromJsonAsync<MedicalStaffDto>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldNotBeNull();
        result.DoctorId.ShouldBe(doctor.DoctorId);
    }
    
    [Fact]
    public async Task CreateDoctor_ShouldReturn_Created_WhenUserIsAdmin()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Doctor(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.SaveChangesAsync();

        var doctorDto = new MedicalStaffDto
        {
            DoctorId = Guid.NewGuid(),
            Name = "Test",
            Surname = "Test",
            PrivatePhoneNumber = "+48123456789",
            HospitalPhoneNumber = "+48987654321",
            Specialization = "Test",
            MedicalLicenseNumber = "ABC123",
            UserId = user.UserId,
            DepartmentId = department.DepartmentId
        };

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PostAsJsonAsync("/users-module/MedicalStaff", doctorDto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task UpdateDoctor_ShouldReturn_NoContent_WhenDoctorUpdatedSuccessfully()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Doctor(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var doctor = new MedicalStaff { DoctorId = Guid.NewGuid(), Name = "Test", Surname = "Test", PrivatePhoneNumber = "+48123456789", HospitalPhoneNumber = "+48987654321", Specialization = "Test", MedicalLicenseNumber = "ABC123", UserId = user.UserId, DepartmentId = department.DepartmentId };
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.MedicalStaff.AddAsync(doctor);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        var updatedDoctorDto = new MedicalStaffDto { DoctorId = doctor.DoctorId, Name = "Test 2", Surname = "Test 2", PrivatePhoneNumber = "+48123456789", HospitalPhoneNumber = "+48987654321", Specialization = "Test", MedicalLicenseNumber = "ABC123", UserId = user.UserId, DepartmentId = department.DepartmentId };

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.PutAsJsonAsync($"/users-module/MedicalStaff/{doctor.DoctorId}", updatedDoctorDto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task DeleteDoctor_ShouldReturn_NoContent_WhenDoctorDeletedSuccessfully()
    {
        // Arrange
        var admin = new User(Guid.NewGuid(), "admin@test.com", "password", Role.Admin(), true, DateTime.Now);
        var user = new User(Guid.NewGuid(), "user1@test.com", "password", Role.Doctor(), true, DateTime.Now);
        var department = new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurologia", NumberOfBeds = 50, ActualNumberOfPatients = 20};
        var doctor = new MedicalStaff { DoctorId = Guid.NewGuid(), Name = "Test", Surname = "Test", PrivatePhoneNumber = "+48123456789", HospitalPhoneNumber = "+48987654321", Specialization = "Test", MedicalLicenseNumber = "ABC123", UserId = user.UserId, DepartmentId = department.DepartmentId };
        await _testDatabase.DbContext.Users.AddAsync(admin);
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.Departments.AddAsync(department);
        await _testDatabase.DbContext.MedicalStaff.AddAsync(doctor);
        await _testDatabase.DbContext.SaveChangesAsync();

        // Act
        Authorize(admin.UserId, admin.Role);
        var response = await Client.DeleteAsync($"/users-module/MedicalStaff/{doctor.DoctorId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}