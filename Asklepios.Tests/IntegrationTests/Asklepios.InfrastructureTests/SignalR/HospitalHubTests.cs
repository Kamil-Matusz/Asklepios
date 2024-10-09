using Asklepios.Application.Services.Users;
using Asklepios.Application.SignalR;
using Asklepios.Core.Exceptions.Users;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace Asklepios.InfrastructureTests.SignalR;

public class HospitalHubTests
{
    private readonly Mock<IMedicalStaffService> _medicalStaffServiceMock;
    private readonly Mock<HubCallerContext> _hubCallerContextMock;
    private readonly Mock<IHubCallerClients> _clientsMock;
    private readonly Mock<IGroupManager> _groupsMock;
    private readonly HospitalHub _hospitalHub;

    public HospitalHubTests()
    {
        _medicalStaffServiceMock = new Mock<IMedicalStaffService>();
        _hubCallerContextMock = new Mock<HubCallerContext>();
        _clientsMock = new Mock<IHubCallerClients>();
        _groupsMock = new Mock<IGroupManager>();

        _hospitalHub = new HospitalHub(_medicalStaffServiceMock.Object)
        {
            Context = _hubCallerContextMock.Object,
            Clients = _clientsMock.Object,
            Groups = _groupsMock.Object
        };
    }

    [Fact]
    public async Task JoinDoctorGroup_ShouldAddConnectionIdToGroup()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var connectionId = "test-connection-id";
        
        _hubCallerContextMock.Setup(c => c.ConnectionId).Returns(connectionId);
        _medicalStaffServiceMock.Setup(service => service.GetUserIdByDoctorAsync(doctorId)).ReturnsAsync(userId);

        // Act
        await _hospitalHub.JoinDoctorGroup(doctorId);

        // Assert
        _groupsMock.Verify(g => g.AddToGroupAsync(connectionId, userId.ToString(), default), Times.Once);
    }

    [Fact]
    public async Task NotifyDoctorAboutNewPatient_ShouldSendNotificationToCorrectGroup()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var message = "New patient notification";

        _medicalStaffServiceMock.Setup(service => service.GetUserIdByDoctorAsync(doctorId)).ReturnsAsync(userId);
        var mockClientProxy = new Mock<IClientProxy>();

        _clientsMock.Setup(clients => clients.Group(userId.ToString())).Returns(mockClientProxy.Object);

        // Act
        await _hospitalHub.NotifyDoctorAboutNewPatient(doctorId, message);

        // Assert
        mockClientProxy.Verify(client => client.SendCoreAsync("ReceiveNotification", 
            It.Is<object[]>(o => o.Length == 1 && (string)o[0] == message), default), Times.Once);
    }

    [Fact]
    public async Task NotifyDoctorAboutNewPatient_ShouldThrowUserNotFoundException_WhenUserNotFound()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        _medicalStaffServiceMock.Setup(service => service.GetUserIdByDoctorAsync(doctorId)).ReturnsAsync(Guid.Empty);

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _hospitalHub.NotifyDoctorAboutNewPatient(doctorId, "Test message"));
    }
}