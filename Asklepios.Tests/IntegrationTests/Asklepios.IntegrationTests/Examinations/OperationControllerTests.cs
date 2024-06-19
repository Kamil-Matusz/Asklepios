using System.Net;
using Shouldly;

namespace Asklepios.IntegrationTests.Examinations;

[Collection("examination")]
public class OperationControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public OperationControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task GetOperationById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = Guid.NewGuid();
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/examinations-module/Operations/{id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}