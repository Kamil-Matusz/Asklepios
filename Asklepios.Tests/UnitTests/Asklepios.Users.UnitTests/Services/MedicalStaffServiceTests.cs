using Asklepios.Application.Services.Users;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Policies.Users;
using Asklepios.Core.Repositories.Users;
using Moq;

namespace Asklepios.Users.UnitTests.Services;

public class MedicalStaffServiceTests
{
    [Fact]
    public async Task AddDoctorAsync_WhenCannotCreateDoctor_ShouldThrowCannotCreateDoctorException()
    {
        // Arrange
        var rolePolicyMock = new Mock<IRolePolicy>();
        var medicalStaffRepositoryMock = new Mock<IMedicalStaffRepository>();
        var medicalStaffCacheRepositoryMock = new Mock<IMedicalStaffCacheRepository>();

        var medicalStaffService = new MedicalStaffService(medicalStaffRepositoryMock.Object, rolePolicyMock.Object, medicalStaffCacheRepositoryMock.Object);

        var userId = Guid.NewGuid();
        var doctorDto = new MedicalStaffDto
        {
            UserId = userId,
            Name = "John",
            Surname = "Doe",
            PrivatePhoneNumber = "+123456789",
            HospitalPhoneNumber = "+987654321",
            Specialization = "Kardiologia",
            MedicalLicenseNumber = "ABC123",
            DepartmentId = Guid.NewGuid()
        };

        rolePolicyMock.Setup(rp => rp.CannotCreateDoctor(userId)).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<CannotCreateDoctorException>(() => medicalStaffService.AddDoctorAsync(doctorDto));
    }
    
    [Fact]
    public async Task AddDoctorAsync_WhenCanCreateDoctor_ShouldAddDoctor()
    {
        // Arrange
        var rolePolicyMock = new Mock<IRolePolicy>();
        var medicalStaffRepositoryMock = new Mock<IMedicalStaffRepository>();
        var medicalStaffCacheRepositoryMock = new Mock<IMedicalStaffCacheRepository>();

        var medicalStaffService = new MedicalStaffService(medicalStaffRepositoryMock.Object, rolePolicyMock.Object, medicalStaffCacheRepositoryMock.Object);

        var userId = Guid.NewGuid();
        var doctorDto = new MedicalStaffDto
        {
            UserId = userId,
            Name = "John",
            Surname = "Doe",
            PrivatePhoneNumber = "+123456789",
            HospitalPhoneNumber = "+987654321",
            Specialization = "Kardiologia",
            MedicalLicenseNumber = "ABC123",
            DepartmentId = Guid.NewGuid()
        };

        rolePolicyMock.Setup(rp => rp.CannotCreateDoctor(userId)).ReturnsAsync(true);

        // Act
        await medicalStaffService.AddDoctorAsync(doctorDto);

        // Assert
        medicalStaffRepositoryMock.Verify(
            mr => mr.AddDoctorAsync(
                It.Is<MedicalStaff>(doctor =>
                    doctor.Name == doctorDto.Name &&
                    doctor.Surname == doctorDto.Surname &&
                    doctor.PrivatePhoneNumber == doctorDto.PrivatePhoneNumber &&
                    doctor.HospitalPhoneNumber == doctorDto.HospitalPhoneNumber &&
                    doctor.Specialization == doctorDto.Specialization &&
                    doctor.MedicalLicenseNumber == doctorDto.MedicalLicenseNumber &&
                    doctor.UserId == doctorDto.UserId &&
                    doctor.DepartmentId == doctorDto.DepartmentId)),
            Times.Once);
    }
    
    [Fact]
    public async Task DeleteDoctorAsync_WhenDoctorDoesNotExist_ShouldThrowDoctorNotFoundException()
    {
        // Arrange
        var medicalStaffRepositoryMock = new Mock<IMedicalStaffRepository>();
        var medicalStaffCacheRepositoryMock = new Mock<IMedicalStaffCacheRepository>();
        var medicalStaffService = new MedicalStaffService(medicalStaffRepositoryMock.Object, null, medicalStaffCacheRepositoryMock.Object);

        var doctorId = Guid.NewGuid();
        medicalStaffRepositoryMock.Setup(msr => msr.GetDoctorByIdAsync(doctorId)).ReturnsAsync((MedicalStaff) null);

        // Act & Assert
        await Assert.ThrowsAsync<DoctorNotFoundException>(() => medicalStaffService.DeleteDoctorAsync(doctorId));
    }

    [Fact]
    public async Task DeleteDoctorAsync_WhenDoctorExists_ShouldDeleteDoctor()
    {
        // Arrange
        var medicalStaffRepositoryMock = new Mock<IMedicalStaffRepository>();
        var medicalStaffCacheRepositoryMock = new Mock<IMedicalStaffCacheRepository>();
        var medicalStaffService = new MedicalStaffService(medicalStaffRepositoryMock.Object, null, medicalStaffCacheRepositoryMock.Object);

        var doctorId = Guid.NewGuid();
        var doctor = new MedicalStaff { DoctorId = doctorId };
        medicalStaffRepositoryMock.Setup(msr => msr.GetDoctorByIdAsync(doctorId)).ReturnsAsync(doctor);

        // Act
        await medicalStaffService.DeleteDoctorAsync(doctorId);

        // Assert
        medicalStaffRepositoryMock.Verify(msr => msr.DeleteDoctorAsync(doctor), Times.Once);
    }
    
    [Fact]
    public async Task UpdateDoctorAsync_WhenDoctorDoesNotExist_ShouldThrowDoctorNotFoundException()
    {
        // Arrange
        var medicalStaffRepositoryMock = new Mock<IMedicalStaffRepository>();
        var medicalStaffCacheRepositoryMock = new Mock<IMedicalStaffCacheRepository>();
        var medicalStaffService = new MedicalStaffService(medicalStaffRepositoryMock.Object, null, medicalStaffCacheRepositoryMock.Object);

        var doctorId = Guid.NewGuid();
        medicalStaffRepositoryMock.Setup(msr => msr.GetDoctorByIdAsync(doctorId)).ReturnsAsync((MedicalStaff)null); // Symulujemy brak lekarza w repozytorium

        var doctorDto = new MedicalStaffDto
        {
            DoctorId = doctorId,
            Name = "John",
            Surname = "Doe",
            PrivatePhoneNumber = "+123456789",
            HospitalPhoneNumber = "+987654321",
            Specialization = "Kardiologia",
            MedicalLicenseNumber = "ABC123"
        };

        // Act & Assert
        await Assert.ThrowsAsync<DoctorNotFoundException>(() => medicalStaffService.UpdateDoctorAsync(doctorDto));
    }
    
    [Fact]
    public async Task UpdateDoctorAsync_WhenDoctorExists_ShouldUpdateDoctor()
    {
        // Arrange
        var medicalStaffRepositoryMock = new Mock<IMedicalStaffRepository>();
        var medicalStaffCacheRepositoryMock = new Mock<IMedicalStaffCacheRepository>();
        var medicalStaffService = new MedicalStaffService(medicalStaffRepositoryMock.Object, null, medicalStaffCacheRepositoryMock.Object);

        var doctorId = Guid.NewGuid();
        var doctor = new MedicalStaff { DoctorId = doctorId };
        medicalStaffRepositoryMock.Setup(msr => msr.GetDoctorByIdAsync(doctorId)).ReturnsAsync(doctor);

        var doctorDto = new MedicalStaffDto
        {
            DoctorId = doctorId,
            Name = "Frank",
            Surname = "Doe",
            PrivatePhoneNumber = "+123456789",
            HospitalPhoneNumber = "+987654321",
            Specialization = "Anestezjologia",
            MedicalLicenseNumber = "ABC456"
        };

        // Act
        await medicalStaffService.UpdateDoctorAsync(doctorDto);

        // Assert
        Assert.Equal("Frank", doctor.Name);
        Assert.Equal("Doe", doctor.Surname);
        Assert.Equal("Anestezjologia", doctor.Specialization);
        Assert.Equal("ABC456", doctor.MedicalLicenseNumber);
    }
}