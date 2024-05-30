using Asklepios.Application.Commands;
using Asklepios.Application.Commands.Users;
using Shouldly;

namespace Asklepios.Users.UnitTests.Commands;

public class SignInTests
{
    [Fact]
    public void signIn_shouldSetProperties_correctly()
    {
        // Arrange
        var email = "testemail@test.com";
        var password = "password";

        // Act
        var signInCommand = new SignIn(email, password);

        // Assert
        signInCommand.Email.ShouldBe(email);
        signInCommand.Password.ShouldBe(password);
    }
}