using Asklepios.Application.Services.Clinics;
using Asklepios.Application.Services.Email;
using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.Exceptions.Clinics;
using Asklepios.Core.Repositories.Clinics;
using Moq;

namespace Asklepios.Clinics.UnitTests.Services
{
    public class ClinicAppointmentServiceTests
    {
        [Fact]
        public async Task RegisterPatientAndCreateAppointmentAsync_WhenPatientDoesNotExist_ShouldRegisterPatientAndCreateAppointment()
        {
            // Arrange
            var clinicPatientRepositoryMock = new Mock<IClinicPatientRepository>();
            var clinicAppointmentRepositoryMock = new Mock<IClinicAppointmentRepository>();
            var emailServiceMock = new Mock<IEmailService>();

            var dto = new ClinicAppointmentRequestDto
            {
                PatientName = "John",
                PatientSurname = "Doe",
                PeselNumber = "12345678901",
                ContactNumber = "123456789",
                Email = "john@example.com",
                AppointmentDate = DateTime.Now,
                AppointmentType = "Consultation",
                MedicalStaffId = Guid.NewGuid()
            };

            var service = new ClinicAppointmentService(clinicPatientRepositoryMock.Object, clinicAppointmentRepositoryMock.Object, emailServiceMock.Object);

            clinicPatientRepositoryMock.Setup(repo => repo.GetPatientByPeselAsync(dto.PeselNumber)).ReturnsAsync((ClinicPatient)null);

            // Act
            await service.RegisterPatientAndCreateAppointmentAsync(dto);

            // Assert
            clinicPatientRepositoryMock.Verify(repo => repo.AddClinicPatientAsync(It.Is<ClinicPatient>(p =>
                p.PatientName == dto.PatientName &&
                p.PatientSurname == dto.PatientSurname &&
                p.PeselNumber == dto.PeselNumber &&
                p.ContactNumber == dto.ContactNumber &&
                p.Email == dto.Email)), Times.Once);

            clinicAppointmentRepositoryMock.Verify(repo => repo.AddAppointmentAsync(It.IsAny<ClinicAppointment>()), Times.Once);
            emailServiceMock.Verify(service => service.SendEmailWithConfirmVisit(dto.Email, dto.PatientName, dto.PatientSurname, dto.AppointmentDate.ToString("yyyy-MM-dd")), Times.Once);
        }

        [Fact]
        public async Task DeleteClinicAppointmentAsync_WhenAppointmentDoesNotExist_ShouldThrowClinicAppointmentNotFoundException()
        {
            // Arrange
            var clinicAppointmentRepositoryMock = new Mock<IClinicAppointmentRepository>();
            var service = new ClinicAppointmentService(null, clinicAppointmentRepositoryMock.Object, null);

            var appointmentId = Guid.NewGuid();
            clinicAppointmentRepositoryMock.Setup(repo => repo.GetAppointmentByIdAsync(appointmentId)).ReturnsAsync((ClinicAppointment)null);

            // Act & Assert
            await Assert.ThrowsAsync<ClinicAppointmentNotFoundException>(() => service.DeleteClinicAppointmentAsync(appointmentId));
        }

        [Fact]
        public async Task DeleteClinicAppointmentAsync_WhenAppointmentExists_ShouldDeleteAppointment()
        {
            // Arrange
            var clinicAppointmentRepositoryMock = new Mock<IClinicAppointmentRepository>();
            var service = new ClinicAppointmentService(null, clinicAppointmentRepositoryMock.Object, null);

            var appointmentId = Guid.NewGuid();
            var clinicAppointment = new ClinicAppointment { AppointmentId = appointmentId };
            clinicAppointmentRepositoryMock.Setup(repo => repo.GetAppointmentByIdAsync(appointmentId)).ReturnsAsync(clinicAppointment);

            // Act
            await service.DeleteClinicAppointmentAsync(appointmentId);

            // Assert
            clinicAppointmentRepositoryMock.Verify(repo => repo.DeleteAppointmentAsync(clinicAppointment), Times.Once);
        }

        [Fact]
        public async Task GetClinicAppointmentByIdAsync_WhenAppointmentDoesNotExist_ShouldThrowClinicAppointmentNotFoundException()
        {
            // Arrange
            var clinicAppointmentRepositoryMock = new Mock<IClinicAppointmentRepository>();
            var service = new ClinicAppointmentService(null, clinicAppointmentRepositoryMock.Object, null);

            var appointmentId = Guid.NewGuid();
            clinicAppointmentRepositoryMock.Setup(repo => repo.GetAppointmentByIdAsync(appointmentId)).ReturnsAsync((ClinicAppointment)null);

            // Act & Assert
            await Assert.ThrowsAsync<ClinicAppointmentNotFoundException>(() => service.GetClinicAppointmentByIdAsync(appointmentId));
        }

        [Fact]
        public async Task UpdateClinicAppointmentAsync_WhenAppointmentDoesNotExist_ShouldThrowClinicAppointmentNotFoundException()
        {
            // Arrange
            var clinicAppointmentRepositoryMock = new Mock<IClinicAppointmentRepository>();
            var service = new ClinicAppointmentService(null, clinicAppointmentRepositoryMock.Object, null);

            var dto = new ClinicAppointmentStatusDto { AppointmentId = Guid.NewGuid(), Status = "Completed" };

            clinicAppointmentRepositoryMock.Setup(repo => repo.GetAppointmentByIdAsync(dto.AppointmentId)).ReturnsAsync((ClinicAppointment)null);

            // Act & Assert
            await Assert.ThrowsAsync<ClinicAppointmentNotFoundException>(() => service.UpdateClinicAppointmentAsync(dto));
        }

        [Fact]
        public async Task UpdateClinicAppointmentAsync_WhenAppointmentExists_ShouldUpdateAppointmentStatus()
        {
            // Arrange
            var clinicAppointmentRepositoryMock = new Mock<IClinicAppointmentRepository>();
            var service = new ClinicAppointmentService(null, clinicAppointmentRepositoryMock.Object, null);

            var dto = new ClinicAppointmentStatusDto { AppointmentId = Guid.NewGuid(), Status = "Completed" };
            var clinicAppointment = new ClinicAppointment { AppointmentId = dto.AppointmentId, Status = "Scheduled" };

            clinicAppointmentRepositoryMock.Setup(repo => repo.GetAppointmentByIdAsync(dto.AppointmentId)).ReturnsAsync(clinicAppointment);

            // Act
            await service.UpdateClinicAppointmentAsync(dto);

            // Assert
            clinicAppointmentRepositoryMock.Verify(repo => repo.UpdateAppointmentAsync(It.Is<ClinicAppointment>(a => a.Status == "Completed")), Times.Once);
            Assert.Equal("Completed", clinicAppointment.Status);
        }

        [Fact]
        public async Task GetClinicAppointmentsByDateAsync_WhenNoAppointmentsExist_ShouldReturnEmptyList()
        {
            // Arrange
            var clinicAppointmentRepositoryMock = new Mock<IClinicAppointmentRepository>();
            var service = new ClinicAppointmentService(null, clinicAppointmentRepositoryMock.Object, null);

            var date = DateTime.Today;

            clinicAppointmentRepositoryMock.Setup(repo => repo.GetAppointmentsByDateAsync(date)).ReturnsAsync(new List<ClinicAppointment>());

            // Act
            var result = await service.GetClinicAppointmentsByDateAsync(date);

            // Assert
            Assert.Empty(result);
        }
    }
}