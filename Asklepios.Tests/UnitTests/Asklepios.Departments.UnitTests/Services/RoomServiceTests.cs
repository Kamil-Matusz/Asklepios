using Asklepios.Application.Services.Departments;
using Asklepios.Core.DTO.Departments;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Exceptions.Departments;
using Asklepios.Core.Repositories.Departments;
using Moq;

namespace Asklepios.Departments.UnitTests.Services;

public class RoomServiceTests
{
    [Fact]
    public async Task AddRoomAsync_ShouldAddRoom()
    {
        // Arrange
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var roomService = new RoomService(roomRepositoryMock.Object);

        var roomDto = new RoomDto
        {
            DepartmentId = Guid.NewGuid(),
            RoomNumber = 102,
            RoomType = "ICU",
            NumberOfBeds = 2
        };

        // Act
        await roomService.AddRoomAsync(roomDto);

        // Assert
        roomRepositoryMock.Verify(rr => rr.AddRoomAsync(It.IsAny<Room>()), Times.Once);
        Assert.NotEqual(Guid.Empty, roomDto.RoomId);  // Ensure RoomId is generated
    }
    
    [Fact]
    public async Task GetRoomAsync_WhenRoomExists_ShouldReturnRoomDetails()
    {
        // Arrange
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var roomService = new RoomService(roomRepositoryMock.Object);

        var roomId = Guid.NewGuid();
        var room = new Room
        {
            RoomId = roomId,
            DepartmentId = Guid.NewGuid(),
            RoomNumber = 205,
            RoomType = "ICU",
            NumberOfBeds = 2,
            Patients = new List<Patient>
            {
                new Patient { PatientId = Guid.NewGuid(), PatientName = "John", PatientSurname = "Doe", InitialDiagnosis = "Flu", IsDischarged = false, Treatment = "Rest" }
            }
        };

        roomRepositoryMock.Setup(rr => rr.GetRoomByIdAsync(roomId)).ReturnsAsync(room);

        // Act
        var result = await roomService.GetRoomAsync(roomId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(205, result.RoomNumber);
        Assert.Equal("ICU", result.RoomType);
        Assert.Single(result.Patients);
        Assert.Equal("John", result.Patients.First().PatientName);
        roomRepositoryMock.Verify(rr => rr.GetRoomByIdAsync(roomId), Times.Once);
    }
    
    [Fact]
    public async Task UpdateRoomAsync_WhenRoomExists_ShouldUpdateRoom()
    {
        // Arrange
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var roomService = new RoomService(roomRepositoryMock.Object);

        var roomId = Guid.NewGuid();
        var room = new Room
        {
            RoomId = roomId,
            RoomNumber = 101,
            RoomType = "ICU",
            NumberOfBeds = 2,
            DepartmentId = Guid.NewGuid()
        };

        roomRepositoryMock.Setup(rr => rr.GetRoomByIdAsync(roomId)).ReturnsAsync(room);

        var roomDto = new RoomDto
        {
            RoomId = roomId,
            RoomNumber = 102,
            RoomType = "Surgical",
            NumberOfBeds = 4,
            DepartmentId = room.DepartmentId
        };

        // Act
        await roomService.UpdateRoomAsync(roomDto);

        // Assert
        roomRepositoryMock.Verify(rr => rr.UpdateRoomAsync(It.IsAny<Room>()), Times.Once);
        Assert.Equal(102, room.RoomNumber);
        Assert.Equal("Surgical", room.RoomType);
        Assert.Equal(4, room.NumberOfBeds);
    }
    
    [Fact]
    public async Task DeleteRoomAsync_WhenRoomExists_ShouldDeleteRoom()
    {
        // Arrange
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var roomService = new RoomService(roomRepositoryMock.Object);

        var roomId = Guid.NewGuid();
        var room = new Room { RoomId = roomId };

        roomRepositoryMock.Setup(rr => rr.GetRoomByIdAsync(roomId)).ReturnsAsync(room);

        // Act
        await roomService.DeleteRoomAsync(roomId);

        // Assert
        roomRepositoryMock.Verify(rr => rr.DeleteRoomAsync(room), Times.Once);
    }
    
    [Fact]
    public async Task DeleteRoomAsync_WhenRoomDoesNotExist_ShouldThrowRoomNotFoundException()
    {
        // Arrange
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var roomService = new RoomService(roomRepositoryMock.Object);

        var roomId = Guid.NewGuid();

        roomRepositoryMock.Setup(rr => rr.GetRoomByIdAsync(roomId)).ReturnsAsync((Room)null);

        // Act & Assert
        await Assert.ThrowsAsync<RoomNotFoundException>(() => roomService.DeleteRoomAsync(roomId));
        roomRepositoryMock.Verify(rr => rr.GetRoomByIdAsync(roomId), Times.Once);
    }
}