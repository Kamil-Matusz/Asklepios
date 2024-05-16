using Asklepios.Application.Commands.Discharges;
using Shouldly;

namespace Asklepios.Patients.UnitTests.Commands;

public class DeleteDischargeTest
{
    [Fact]
    public void deleteDischarge_shouldSetProperties_correctly()
    {
        // Arrange
        var dischargeId = Guid.NewGuid();
        
        // Act
        var deleteDischargeCommand = new DeleteDischarge(dischargeId);
        
        // Assert
        deleteDischargeCommand.DischargeId.ShouldBe(dischargeId);
    }
}