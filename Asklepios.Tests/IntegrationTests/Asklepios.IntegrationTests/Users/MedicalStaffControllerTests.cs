using System.Net;
using Shouldly;

namespace Asklepios.IntegrationTests.Users;

[Collection("users")]
public class MedicalStaffControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public MedicalStaffControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task GetDoctorById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = Guid.NewGuid();
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/users-module/MedicalStaff/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}