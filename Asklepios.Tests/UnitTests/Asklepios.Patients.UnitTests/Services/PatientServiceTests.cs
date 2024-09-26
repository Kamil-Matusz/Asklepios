using Asklepios.Application.Services.Patients;
using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Exceptions.Patients;
using Asklepios.Core.Repositories.Patients;
using Moq;

namespace Asklepios.Patients.UnitTests.Services;

public class PatientServiceTests
{
    [Fact]
    public async Task GetAllPatientsAsync_ReturnsEmptyList_WhenNoPatientsExist()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientService = new PatientService(patientRepositoryMock.Object, null, null);

        patientRepositoryMock.Setup(pr => pr.GetAllPatientsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Patient>());

        // Act
        var result = await patientService.GetAllPatientsAsync(0, 10);

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task DeletePatientAsync_WhenPatientDoesNotExist_ShouldThrowPatientNotFoundException()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientId = Guid.NewGuid();
        var patientService = new PatientService(patientRepositoryMock.Object, null, null);

        patientRepositoryMock.Setup(pr => pr.GetPatientByIdAsync(patientId)).ReturnsAsync((Patient) null);

        // Act & Assert
        await Assert.ThrowsAsync<PatientNotFoundException>(() => patientService.DeletePatientAsync(patientId));
    }
    
    [Fact]
    public async Task DeletePatientAsync_WhenPatientExists_ShouldDeletePatient()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientId = Guid.NewGuid();
        var patient = new Patient { PatientId = patientId };

        var patientService = new PatientService(patientRepositoryMock.Object, null, null);

        patientRepositoryMock.Setup(pr => pr.GetPatientByIdAsync(patientId)).ReturnsAsync(patient);

        // Act
        await patientService.DeletePatientAsync(patientId);

        // Assert
        patientRepositoryMock.Verify(pr => pr.DeletePatientAsync(patient), Times.Once);
    }
    
    [Fact]
    public async Task UpdatePatientAsync_WhenPatientDoesNotExist_ShouldThrowPatientNotFoundException()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientId = Guid.NewGuid();

        var patientService = new PatientService(patientRepositoryMock.Object, null, null);

        patientRepositoryMock.Setup(pr => pr.GetPatientByIdAsync(patientId)).ReturnsAsync((Patient) null);

        var patientDto = new PatientDto
        {
            PatientId = patientId,
            PatientName = "John",
            PatientSurname = "Doe",
            PeselNumber = "010300407246",
            InitialDiagnosis = "Fue",
            IsDischarged = false,
            Treatment = "Fue",
            DepartmentId = Guid.NewGuid(),
            RoomId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<PatientNotFoundException>(() => patientService.UpdatePatientAsync(patientDto));
    }

    [Fact]
    public async Task UpdatePatientAsync_WhenPatientExists_ShouldUpdatePatient()
    {
        // Arrange
        var patientRepositoryMock = new Mock<IPatientRepository>();
        var patientId = Guid.NewGuid();
        var patient = new Patient { PatientId = patientId };

        var patientService = new PatientService(patientRepositoryMock.Object, null, null);

        patientRepositoryMock.Setup(pr => pr.GetPatientByIdAsync(patientId)).ReturnsAsync(patient);

        var patientDto = new PatientDto
        {
            PatientId = patientId,
            PatientName = "John",
            PatientSurname = "Doe",
            PeselNumber = "010300407246",
            InitialDiagnosis = "Fue",
            IsDischarged = false,
            Treatment = "Fue",
            DepartmentId = Guid.NewGuid(),
            RoomId = Guid.NewGuid()
        };

        // Act
        await patientService.UpdatePatientAsync(patientDto);

        // Assert
        patientRepositoryMock.Verify(nr => nr.UpdatePatientAsync(patient), Times.Once);
        Assert.Equal("John", patient.PatientName);
        Assert.Equal("Doe", patient.PatientSurname);
        Assert.Equal(patientDto.DepartmentId, patient.DepartmentId);
        Assert.Equal(patientDto.RoomId, patient.RoomId);
    }
}