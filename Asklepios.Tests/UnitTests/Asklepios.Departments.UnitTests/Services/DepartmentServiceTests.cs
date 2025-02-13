using Asklepios.Application.Services.Departments;
using Asklepios.Core.DTO.Departments;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Exceptions.Departments;
using Asklepios.Core.Policies.Departments;
using Asklepios.Core.Repositories.Departments;
using Moq;

namespace Asklepios.Departments.UnitTests.Services;

public class DepartmentServiceTests
{
    [Fact]
    public async Task DeleteDepartmentAsync_WhenDepartmentDoesNotExist_ShouldThrowDepartmentNotFoundException()
    {
        // Arrange
        var departmentRepositoryMock = new Mock<IDepartmentRepository>();
        var departmentId = Guid.NewGuid();
        var departmentService = new DepartmentService(departmentRepositoryMock.Object, null, null);

        departmentRepositoryMock.Setup(dr => dr.GetDepartmentByIdAsync(departmentId))
            .ReturnsAsync((Department)null);

        // Act & Assert
        await Assert.ThrowsAsync<DepartmentNotFoundException>(() => departmentService.DeleteDepartmentAsync(departmentId));
    }
    
    [Fact]
    public async Task DeleteDepartmentAsync_WhenDepartmentExistsAndPolicyAllows_ShouldDeleteDepartment()
    {
        // Arrange
        var departmentRepositoryMock = new Mock<IDepartmentRepository>();
        var departmentDeletePolicyMock = new Mock<IDepartmentDeletePolicy>();
        var departmentId = Guid.NewGuid();
        var department = new Department { DepartmentId = departmentId };

        departmentRepositoryMock.Setup(dr => dr.GetDepartmentByIdAsync(departmentId))
            .ReturnsAsync(department);
        departmentDeletePolicyMock.Setup(dp => dp.CannotDeleteDepartmentPolicy(department))
            .ReturnsAsync(true);

        var departmentService = new DepartmentService(departmentRepositoryMock.Object, departmentDeletePolicyMock.Object, null);

        // Act
        await departmentService.DeleteDepartmentAsync(departmentId);

        // Assert
        departmentRepositoryMock.Verify(dr => dr.DeleteDepartmentAsync(department), Times.Once);
    }
    
    [Fact]
    public async Task AddDepartmentAsync_ShouldAddDepartmentWithNewId()
    {
        // Arrange
        var departmentRepositoryMock = new Mock<IDepartmentRepository>();
        var departmentService = new DepartmentService(departmentRepositoryMock.Object, null, null);

        var departmentDto = new DepartmentDto
        {
            DepartmentName = "Cardiology",
            NumberOfBeds = 50,
            ActualNumberOfPatients = 45
        };

        // Act
        await departmentService.AddDepartmentAsync(departmentDto);

        // Assert
        departmentRepositoryMock.Verify(dr => dr.AddDepartmentAsync(It.Is<Department>(d =>
                d.DepartmentName == "Cardiology" &&
                d.NumberOfBeds == 50 &&
                d.ActualNumberOfPatients == 45 &&
                d.DepartmentId != Guid.Empty
        )), Times.Once);
    }
    
    [Fact]
    public async Task GetDepartmentAsync_WhenDepartmentExists_ShouldReturnDepartmentDetailsDto()
    {
        // Arrange
        var departmentRepositoryMock = new Mock<IDepartmentRepository>();
        var departmentId = Guid.NewGuid();
        var department = new Department
        {
            DepartmentId = departmentId,
            DepartmentName = "Cardiology",
            NumberOfBeds = 50,
            ActualNumberOfPatients = 45,
            Rooms = new List<Room>
            {
                new Room { RoomId = Guid.NewGuid(), RoomNumber = 101, RoomType = "ICU" }
            },
            Patients = new List<Patient>
            {
                new Patient { PatientId = Guid.NewGuid(), PatientName = "John", PatientSurname = "Doe", InitialDiagnosis = "Heart Attack", IsDischarged = false, Treatment = "Treatment A" }
            }
        };

        departmentRepositoryMock.Setup(dr => dr.GetDepartmentByIdAsync(departmentId))
            .ReturnsAsync(department);

        var departmentService = new DepartmentService(departmentRepositoryMock.Object, null, null);

        // Act
        var result = await departmentService.GetDepartmentAsync(departmentId);

        // Assert
        Assert.Equal(departmentId, result.DepartmentId);
        Assert.Equal("Cardiology", result.DepartmentName);
        Assert.Equal(50, result.NumberOfBeds);
        Assert.Single(result.Rooms);
        Assert.Single(result.Patients);
        Assert.Equal(101, result.Rooms[0].RoomNumber);
        Assert.Equal("John", result.Patients[0].PatientName);
    }
    
    [Fact]
    public async Task UpdateDepartmentAsync_WhenDepartmentExists_ShouldUpdateDepartment()
    {
        // Arrange
        var departmentRepositoryMock = new Mock<IDepartmentRepository>();
        var departmentId = Guid.NewGuid();
        var department = new Department { DepartmentId = departmentId };

        departmentRepositoryMock.Setup(dr => dr.GetDepartmentByIdAsync(departmentId))
            .ReturnsAsync(department);

        var departmentService = new DepartmentService(departmentRepositoryMock.Object, null, null);

        var departmentDto = new DepartmentDetailsDto
        {
            DepartmentId = departmentId,
            DepartmentName = "Cardiology",
            NumberOfBeds = 20
        };

        // Act
        await departmentService.UpdateDepartmentAsync(departmentDto);

        // Assert
        departmentRepositoryMock.Verify(dr => dr.UpdateDepartmentAsync(department), Times.Once);
        Assert.Equal("Cardiology", department.DepartmentName);
        Assert.Equal(20, department.NumberOfBeds);
    }
    
    [Fact]
    public async Task UpdateDepartmentAsync_WhenDepartmentDoesNotExist_ShouldThrowDepartmentNotFoundException()
    {
        // Arrange
        var departmentRepositoryMock = new Mock<IDepartmentRepository>();
        var departmentId = Guid.NewGuid();

        departmentRepositoryMock.Setup(dr => dr.GetDepartmentByIdAsync(departmentId))
            .ReturnsAsync((Department)null);

        var departmentService = new DepartmentService(departmentRepositoryMock.Object, null, null);

        var departmentDto = new DepartmentDetailsDto
        {
            DepartmentId = departmentId,
            DepartmentName = "Cardiology",
            NumberOfBeds = 20
        };

        // Act & Assert
        await Assert.ThrowsAsync<DepartmentNotFoundException>(() => departmentService.UpdateDepartmentAsync(departmentDto));
    }
    
    [Fact]
    public async Task GetAllDepartmentsAsync_ShouldReturnPagedDepartmentList()
{
    // Arrange
    var departmentRepositoryMock = new Mock<IDepartmentRepository>();
    var departmentCacheRepositoryMock = new Mock<IDepartmentCacheRepository>();

    var departmentService = new DepartmentService(departmentRepositoryMock.Object, null,departmentCacheRepositoryMock.Object);

    var departments = new List<Department>
    {
        new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Cardiology", NumberOfBeds = 50, ActualNumberOfPatients = 45 },
        new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Oncology", NumberOfBeds = 60, ActualNumberOfPatients = 55 }
    };

    var departmentDtos = departments.Select(d => new DepartmentListDto
    {
        DepartmentId = d.DepartmentId,
        DepartmentName = d.DepartmentName,
        NumberOfBeds = d.NumberOfBeds,
        ActualNumberOfPatients = d.ActualNumberOfPatients
    }).ToList();

    int pageIndex = 0, pageSize = 10;
    departmentCacheRepositoryMock.Setup(cr => cr.GetDepartmentsAsync(pageIndex, pageSize))
        .ReturnsAsync((IReadOnlyList<DepartmentListDto>?)null);

    departmentRepositoryMock.Setup(dr => dr.GetAllDepartmentsAsync(pageIndex, pageSize))
        .ReturnsAsync(departments);

    departmentCacheRepositoryMock.Setup(cr => cr.SetDepartmentsAsync(pageIndex, pageSize, It.IsAny<IReadOnlyList<DepartmentListDto>>()))
        .Returns(Task.CompletedTask);

    // Act
    var result = await departmentService.GetAllDepartmentsAsync(pageIndex, pageSize);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(2, result.Count);
    Assert.Equal("Cardiology", result[0].DepartmentName);
    Assert.Equal("Oncology", result[1].DepartmentName);
    
    departmentCacheRepositoryMock.Verify(cr => cr.GetDepartmentsAsync(pageIndex, pageSize), Times.Once);
    departmentRepositoryMock.Verify(dr => dr.GetAllDepartmentsAsync(pageIndex, pageSize), Times.Once);
    departmentCacheRepositoryMock.Verify(cr => cr.SetDepartmentsAsync(pageIndex, pageSize, It.IsAny<IReadOnlyList<DepartmentListDto>>()), Times.Once);
    }
}