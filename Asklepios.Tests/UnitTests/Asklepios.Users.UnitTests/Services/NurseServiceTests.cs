using Asklepios.Application.Services.Users;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Policies.Users;
using Asklepios.Core.Repositories.Users;
using Moq;

namespace Asklepios.Users.UnitTests.Services;

public class NurseServiceTests
{
    [Fact]
    public async Task AddNurseAsync_WithNurseRoleAndPolicyMet_ShouldAddNurse()
    {
        // Arrange
        var nurseRepositoryMock = new Mock<INurseRepository>();
        var rolePolicyMock = new Mock<IRolePolicy>();

        var nurseService = new NurseService(nurseRepositoryMock.Object, rolePolicyMock.Object);

        var userId = Guid.NewGuid();
        var nurseDto = new NurseDto
        {
            UserId = userId,
            Name = "John",
            Surname = "Doe",
            DepartmentId = Guid.NewGuid()
        };

        rolePolicyMock.Setup(rp => rp.CannotCreateNurse(userId)).ReturnsAsync(true);

        // Act
        await nurseService.AddNurseAsync(nurseDto);

        // Assert
        nurseRepositoryMock.Verify(nr => nr.AddNurseAsync(It.IsAny<Nurse>()), Times.Once);
    }
    
    [Fact]
    public async Task AddNurseAsync_WhenPolicyReturnsFalse_ShouldThrowCannotCreateNurseException()
    {
        // Arrange
        var nurseRepositoryMock = new Mock<INurseRepository>();
        var rolePolicyMock = new Mock<IRolePolicy>();

        var nurseService = new NurseService(nurseRepositoryMock.Object, rolePolicyMock.Object);

        var userId = Guid.NewGuid();
        var nurseDto = new NurseDto
        {
            UserId = userId,
            Name = "John",
            Surname = "Doe",
            DepartmentId = Guid.NewGuid()
        };

        rolePolicyMock.Setup(rp => rp.CannotCreateNurse(userId)).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<CannotCreateNurseException>(() => nurseService.AddNurseAsync(nurseDto));
    }
    
    [Fact]
    public async Task DeleteNurseAsync_WhenNurseDoesNotExist_ShouldThrowNurseNotFoundException()
    {
        // Arrange
        var nurseRepositoryMock = new Mock<INurseRepository>();
        var nurseId = Guid.NewGuid();

        var nurseService = new NurseService(nurseRepositoryMock.Object, null);

        nurseRepositoryMock.Setup(nr => nr.GetNurseByIdAsync(nurseId)).ReturnsAsync((Nurse) null);

        // Act & Assert
        await Assert.ThrowsAsync<NurseNotFoundException>(() => nurseService.DeleteNurseAsync(nurseId));
    }
    
    [Fact]
    public async Task DeleteNurseAsync_WhenNurseExists_ShouldDeleteNurse()
    {
        // Arrange
        var nurseRepositoryMock = new Mock<INurseRepository>();
        var nurseId = Guid.NewGuid();
        var nurse = new Nurse { NurseId = nurseId };

        var nurseService = new NurseService(nurseRepositoryMock.Object, null);

        nurseRepositoryMock.Setup(nr => nr.GetNurseByIdAsync(nurseId)).ReturnsAsync(nurse);

        // Act
        await nurseService.DeleteNurseAsync(nurseId);

        // Assert
        nurseRepositoryMock.Verify(nr => nr.DeleteNurseAsync(nurse), Times.Once);
    }
    
    [Fact]
    public async Task GetAllNursesAsync_ReturnsEmptyList_WhenNoNursesExist()
    {
        // Arrange
        var nurseRepositoryMock = new Mock<INurseRepository>();
        var nurseService = new NurseService(nurseRepositoryMock.Object, null);

        nurseRepositoryMock.Setup(nr => nr.GetAllNursesAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Nurse>());

        // Act
        var result = await nurseService.GetAllNursesAsync(0, 10);

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task UpdateNurseAsync_WhenNurseDoesNotExist_ShouldThrowNurseNotFoundException()
    {
        // Arrange
        var nurseRepositoryMock = new Mock<INurseRepository>();
        var nurseId = Guid.NewGuid();

        var nurseService = new NurseService(nurseRepositoryMock.Object, null);

        nurseRepositoryMock.Setup(nr => nr.GetNurseByIdAsync(nurseId)).ReturnsAsync((Nurse) null);

        var nurseDto = new NurseDto
        {
            NurseId = nurseId,
            Name = "UpdatedName",
            Surname = "UpdatedSurname",
            DepartmentId = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<NurseNotFoundException>(() => nurseService.UpdateNurseAsync(nurseDto));
    }
    
    [Fact]
    public async Task UpdateNurseAsync_WhenNurseExists_ShouldUpdateNurse()
    {
        // Arrange
        var nurseRepositoryMock = new Mock<INurseRepository>();
        var nurseId = Guid.NewGuid();
        var nurse = new Nurse { NurseId = nurseId };

        var nurseService = new NurseService(nurseRepositoryMock.Object, null);

        nurseRepositoryMock.Setup(nr => nr.GetNurseByIdAsync(nurseId)).ReturnsAsync(nurse);

        var nurseDto = new NurseDto
        {
            NurseId = nurseId,
            Name = "Joanna",
            Surname = "Kulig",
            DepartmentId = Guid.NewGuid()
        };

        // Act
        await nurseService.UpdateNurseAsync(nurseDto);

        // Assert
        nurseRepositoryMock.Verify(nr => nr.UpdateNurseAsync(nurse), Times.Once);
        Assert.Equal("Joanna", nurse.Name);
        Assert.Equal("Kulig", nurse.Surname);
        Assert.Equal(nurseDto.DepartmentId, nurse.DepartmentId);
    }
}