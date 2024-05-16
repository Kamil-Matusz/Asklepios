using Asklepios.Application.Commands.Discharges;
using Shouldly;

namespace Asklepios.Patients.UnitTests.Commands;

public class AddDischargeTest
{
    [Fact]
    public void addDischarge_shouldSetProperties_correctly()
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
        var addDischargeCommand = new AddDischarge(dischargeId, patientName, patientSurname, peselNumber, address, date, dischargeReasson, summary, medicalStaffId);

        // Assert
        addDischargeCommand.DischargeId.ShouldBe(dischargeId);
        addDischargeCommand.PatientName.ShouldBe(patientName);
        addDischargeCommand.PatientSurname.ShouldBe(patientSurname);
        addDischargeCommand.PeselNumber.ShouldBe(peselNumber);
        addDischargeCommand.Address.ShouldBe(address);
        addDischargeCommand.Date.ShouldBe(date);
        addDischargeCommand.DischargeReasson.ShouldBe(dischargeReasson);
        addDischargeCommand.Summary.ShouldBe(summary);
        addDischargeCommand.MedicalStaffId.ShouldBe(medicalStaffId);
    }
}