using Asklepios.Application.Commands.Discharges;
using Shouldly;

namespace Asklepios.Patients.UnitTests.Commands;

public class UpdateDischargeTest
{
    [Fact]
    public void updateDischarge_shouldSetProperties_correctly()
    {
        // Arrange
        var dischargeId = Guid.NewGuid();
        var patientName = "Kamil";
        var patientSurname = "Matusz";
        var peselNumber = "01300407254";
        var address = "Małszałkowska 3/25 35-211 Rzeszów";
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        var dischargeReasson = "Zakończenie badań";
        var summary = "Wszystko ok";
        var medicalStaffId = Guid.NewGuid();
        
        // Act
        var updateDischargeCommand = new UpdateDischarge(dischargeId, patientName, patientSurname, peselNumber, address, date, dischargeReasson, summary, medicalStaffId);
        
        // Assert
        updateDischargeCommand.DischargeId.ShouldBe(dischargeId);
        updateDischargeCommand.PatientName.ShouldBe(patientName);
        updateDischargeCommand.PatientSurname.ShouldBe(patientSurname);
        updateDischargeCommand.PeselNumber.ShouldBe(peselNumber);
        updateDischargeCommand.Address.ShouldBe(address);
        updateDischargeCommand.Date.ShouldBe(date);
        updateDischargeCommand.DischargeReasson.ShouldBe(dischargeReasson);
        updateDischargeCommand.Summary.ShouldBe(summary);
        updateDischargeCommand.MedicalStaffId.ShouldBe(medicalStaffId);
    }
}