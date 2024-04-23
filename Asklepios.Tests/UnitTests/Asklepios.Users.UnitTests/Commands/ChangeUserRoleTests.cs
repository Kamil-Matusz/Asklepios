using Asklepios.Application.Commands;
using Shouldly;

namespace Asklepios.Users.UnitTests.Commands;

public class ChangeUserRoleTests
{
    [Fact]
    public void ChangeUserRole_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var role = "NewRole";

        // Act
        var changeUserRoleCommand = new ChangeUserRole(userId, role);

        // Assert
        changeUserRoleCommand.UserId.ShouldBe(userId);
        changeUserRoleCommand.Role.ShouldBe(role);
    }
}