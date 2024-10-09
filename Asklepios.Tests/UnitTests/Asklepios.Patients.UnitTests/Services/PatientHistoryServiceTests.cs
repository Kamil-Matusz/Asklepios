using Asklepios.Application.Services.Patients;
using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Repositories.Patients;
using Moq;
using Newtonsoft.Json;

namespace Asklepios.Patients.UnitTests.Services
{
    public class PatientHistoryServiceTests
    {
        [Fact]
        public async Task AddOrUpdatePatientHistoryAsync_ShouldAddNewPatientHistory_WhenPatientDoesNotExist()
        {
            // Arrange
            var patientHistoryRepositoryMock = new Mock<IPatientHistoryRepository>();
            var patientHistoryService = new PatientHistoryService(patientHistoryRepositoryMock.Object);

            var newVisit = new PatientVisitDto
            {
                AdmissionDate = new DateOnly(2023, 01, 15),
                DischargeDate = new DateOnly(2023, 01, 20),
                OperationName = "Appendectomy",
                Result = "Successful",
                Comlications = "None",
                AnesthesiaType = "General"
            };

            var newHistoryDto = new PatientHistoryDto
            {
                HistoryId = Guid.NewGuid(),
                PatientName = "John",
                PatientSurname = "Doe",
                PeselNumber = "12345678901",
                History = new List<PatientVisitDto> { newVisit }
            };

            patientHistoryRepositoryMock.Setup(pr => pr.PatientExistAsync(newHistoryDto.PeselNumber))
                                        .ReturnsAsync(false);

            // Act
            await patientHistoryService.AddOrUpdatePatientHistoryAsync(newHistoryDto);

            // Assert
            patientHistoryRepositoryMock.Verify(pr => pr.AddPatientHistoryAsync(It.Is<PatientHistory>(ph =>
                ph.PatientName == "John" &&
                ph.PatientSurname == "Doe" &&
                ph.PeselNumber == "12345678901" &&
                ph.History == JsonConvert.SerializeObject(new List<PatientVisitDto> { newVisit })
            )), Times.Once);
        }

        [Fact]
        public async Task GetFullPatientHistoryByPeselAsync_ShouldReturnCorrectHistory_WhenPatientExists()
        {
            // Arrange
            var patientHistoryRepositoryMock = new Mock<IPatientHistoryRepository>();
            var patientHistoryService = new PatientHistoryService(patientHistoryRepositoryMock.Object);

            var existingVisit = new PatientVisitDto
            {
                AdmissionDate = new DateOnly(2022, 12, 01),
                DischargeDate = new DateOnly(2022, 12, 05),
                OperationName = "Tonsillectomy",
                Result = "Successful",
                Comlications = "None",
                AnesthesiaType = "Local"
            };

            var existingPatientHistory = new PatientHistory
            {
                HistoryId = Guid.NewGuid(),
                PatientName = "John",
                PatientSurname = "Doe",
                PeselNumber = "12345678901",
                History = JsonConvert.SerializeObject(new List<PatientVisitDto> { existingVisit })
            };

            patientHistoryRepositoryMock.Setup(pr => pr.GetFullPatientHistoryByPeselAsync(existingPatientHistory.PeselNumber))
                                        .ReturnsAsync(existingPatientHistory);

            // Act
            var result = await patientHistoryService.GetFullPatientHistoryByPeselAsync(existingPatientHistory.PeselNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.PatientName);
            Assert.Equal("Doe", result.PatientSurname);
            Assert.Equal(existingVisit.OperationName, result.History[0].OperationName);
        }
    }
}
