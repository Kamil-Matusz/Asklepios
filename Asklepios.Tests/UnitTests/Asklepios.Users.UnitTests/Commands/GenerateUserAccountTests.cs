using Asklepios.Application.Commands;
using Shouldly;

namespace Asklepios.Users.UnitTests.Commands;

public class GenerateUserAccountTests
{
    [Fact]
    public void generateUserAccount_shouldSetProperties_correctly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var email = "testemail@test.com";
        var role = "Admin";
        var isActive = true;

        // Act
        var generateUserAccountCommand = new GenerateUserAccount(userId, email, role, isActive);

        // Assert
        generateUserAccountCommand.UserId.ShouldBe(userId);
        generateUserAccountCommand.Email.ShouldBe(email);
        generateUserAccountCommand.Role.ShouldBe(role);
        generateUserAccountCommand.IsActive.ShouldBe(isActive);
    }
}