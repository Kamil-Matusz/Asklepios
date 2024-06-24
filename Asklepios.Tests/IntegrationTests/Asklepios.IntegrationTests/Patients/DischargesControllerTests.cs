using System.Net;
using System.Net.Http.Headers;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Shouldly;

namespace Asklepios.IntegrationTests.Patients;

[Collection("discharges")]
public class DischargesControllerTests : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public DischargesControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }
    
    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task GetDischargeById_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
    {
        // Arrange
        var dischargeId = Guid.NewGuid();
        Client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await Client.GetAsync($"/patients-module/Discharges/dischargesById/{dischargeId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}