using Asklepios.Application.Services.Clinics;
using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.Exceptions.Clinics;
using Asklepios.Core.Repositories.Clinics;
using Moq;

namespace Asklepios.Clinics.UnitTests.Services
{
    public class ClinicPatientServiceTests
    {
        [Fact]
        public async Task GetClinicPatientByIdAsync_WhenPatientDoesNotExist_ShouldThrowClinicPatientNotFoundException()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var patientId = Guid.NewGuid();

            var clinicPatientService = new ClinicPatientService(clinicPatientRepositoryMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetClinicPatientByIdAsync(patientId)).ReturnsAsync((ClinicPatient)null);

            // Act & Assert
            await Assert.ThrowsAsync<ClinicPatientNotFoundException>(() => clinicPatientService.GetClinicPatientByIdAsync(patientId));
        }

        [Fact]
        public async Task GetAllClinicPatientsAsync_ReturnsEmptyList_WhenNoPatientsExist()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var clinicPatientService = new ClinicPatientService(clinicPatientRepositoryMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetAllClinicPatientsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<ClinicPatient>());

            // Act
            var result = await clinicPatientService.GetAllClinicPatientsAsync(0, 10);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddClinicPatientAsync_WhenPatientWithSamePeselExists_ShouldThrowClinicPatientAlreadyExistsException()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var existingPesel = "12345678901";
            var dto = new ClinicPatientDto { PeselNumber = existingPesel };

            var clinicPatientService = new ClinicPatientService(clinicPatientRepositoryMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetPatientByPeselAsync(existingPesel)).ReturnsAsync(new ClinicPatient());

            // Act & Assert
            await Assert.ThrowsAsync<ClinicPatientAlreadyExistsException>(() => clinicPatientService.AddClinicPatientAsync(dto));
        }

        [Fact]
        public async Task AddClinicPatientAsync_WhenNoConflict_ShouldAddPatient()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var dto = new ClinicPatientDto
            {
                PatientName = "John",
                PatientSurname = "Doe",
                PeselNumber = "12345678901",
                ContactNumber = "123456789",
                Email = "johndoe@example.com"
            };

            var clinicPatientService = new ClinicPatientService(clinicPatientRepositoryMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetPatientByPeselAsync(dto.PeselNumber)).ReturnsAsync((ClinicPatient)null);

            // Act
            await clinicPatientService.AddClinicPatientAsync(dto);

            // Assert
            clinicPatientRepositoryMock.Verify(repo => repo.AddClinicPatientAsync(It.Is<ClinicPatient>(p =>
                p.PatientName == dto.PatientName &&
                p.PatientSurname == dto.PatientSurname &&
                p.PeselNumber == dto.PeselNumber &&
                p.ContactNumber == dto.ContactNumber &&
                p.Email == dto.Email)), Times.Once);
        }

        [Fact]
        public async Task UpdateClinicPatientAsync_WhenPatientDoesNotExist_ShouldThrowClinicPatientNotFoundException()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var patientId = Guid.NewGuid();

            var clinicPatientService = new ClinicPatientService(clinicPatientRepositoryMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetClinicPatientByIdAsync(patientId)).ReturnsAsync((ClinicPatient)null);

            var dto = new ClinicPatientDto { ClinicPatientId = patientId };

            // Act & Assert
            await Assert.ThrowsAsync<ClinicPatientNotFoundException>(() => clinicPatientService.UpdateClinicPatientAsync(dto));
        }

        [Fact]
        public async Task UpdateClinicPatientAsync_WhenPatientExists_ShouldUpdatePatient()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var patientId = Guid.NewGuid();
            var clinicPatient = new ClinicPatient { ClinicPatientId = patientId, PatientName = "OldName" };

            var clinicPatientService = new ClinicPatientService(clinicPatientRepositoryMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetClinicPatientByIdAsync(patientId)).ReturnsAsync(clinicPatient);

            var dto = new ClinicPatientDto
            {
                ClinicPatientId = patientId,
                PatientName = "NewName",
                PatientSurname = "NewSurname",
                PeselNumber = "12345678901",
                ContactNumber = "123456789",
                Email = "newemail@example.com"
            };

            // Act
            await clinicPatientService.UpdateClinicPatientAsync(dto);

            // Assert
            clinicPatientRepositoryMock.Verify(repo => repo.UpdateClinicPatientAsync(It.Is<ClinicPatient>(p =>
                p.PatientName == "NewName" &&
                p.PatientSurname == "NewSurname" &&
                p.PeselNumber == dto.PeselNumber &&
                p.ContactNumber == dto.ContactNumber &&
                p.Email == dto.Email)), Times.Once);
        }

        [Fact]
        public async Task DeleteClinicPatientAsync_WhenPatientDoesNotExist_ShouldThrowClinicPatientNotFoundException()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var patientId = Guid.NewGuid();

            var clinicPatientService = new ClinicPatientService(clinicPatientRepositoryMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetClinicPatientByIdAsync(patientId)).ReturnsAsync((ClinicPatient)null);

            // Act & Assert
            await Assert.ThrowsAsync<ClinicPatientNotFoundException>(() => clinicPatientService.DeleteClinicPatientAsync(patientId));
        }

        [Fact]
        public async Task DeleteClinicPatientAsync_WhenPatientExists_ShouldDeletePatient()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var patientId = Guid.NewGuid();
            var clinicPatient = new ClinicPatient { ClinicPatientId = patientId };

            var clinicPatientService = new ClinicPatientService(clinicPatientRepositoryMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetClinicPatientByIdAsync(patientId)).ReturnsAsync(clinicPatient);

            // Act
            await clinicPatientService.DeleteClinicPatientAsync(patientId);

            // Assert
            clinicPatientRepositoryMock.Verify(repo => repo.DeleteClinicPatientAsync(clinicPatient), Times.Once);
        }
    }
}
