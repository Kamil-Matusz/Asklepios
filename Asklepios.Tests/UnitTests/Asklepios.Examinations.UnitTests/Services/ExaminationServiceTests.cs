using Asklepios.Application.Services.Examinations;
using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Exceptions.Examinations;
using Asklepios.Core.Repositories.Examinations;
using Asklepios.Core.ValueObjects;
using Moq;

namespace Asklepios.Examinations.UnitTests.Services;

public class ExaminationServiceTests
{
    [Fact]
    public async Task AddExaminationAsync_ShouldAddExam()
    {
        // Arrange
        var examinationRepositoryMock = new Mock<IExaminationRepository>();
        var examinationCacheRepositoryMock = new Mock<IExaminationCacheRepository>();

        var examService = new ExaminationService(examinationRepositoryMock.Object, examinationCacheRepositoryMock.Object);
        
        var examDto = new ExaminationDto()
        {
            ExamId = 1,
            ExamName = "Ogólne",
            ExamCategory = ExamCategory.Func()
        };
        
        // Act
        await examService.AddExaminationAsync(examDto);

        // Assert
        examinationRepositoryMock.Verify(nr => nr.AddExaminationAsync(It.IsAny<Examination>()), Times.Once);
    }
    
    [Fact]
    public async Task DeleteExaminationAsync_WhenExamDoesNotExist_ShouldThrowNurseNotFoundException()
    {
        // Arrange
        var examinationRepositoryMock = new Mock<IExaminationRepository>();
        var examinationCacheRepositoryMock = new Mock<IExaminationCacheRepository>();

        var examService = new ExaminationService(examinationRepositoryMock.Object, examinationCacheRepositoryMock.Object);
        var examId = 1;

        examinationRepositoryMock.Setup(nr => nr.GetExaminationByIdAsync(examId)).ReturnsAsync((Examination) null);

        // Act & Assert
        await Assert.ThrowsAsync<ExaminationNotFoundException>(() => examService.DeleteExaminationAsync(examId));
    }
    
    [Fact]
    public async Task DeleteExamAsync_WhenExamExists_ShouldDeleteExam()
    {
        // Arrange
        var examinationRepositoryMock = new Mock<IExaminationRepository>();
        var examinationCacheRepositoryMock = new Mock<IExaminationCacheRepository>();

        var examService = new ExaminationService(examinationRepositoryMock.Object, examinationCacheRepositoryMock.Object);

        var examId = 1;
        var exam = new Examination { ExaminationId = examId };
        examinationRepositoryMock.Setup(msr => msr.GetExaminationByIdAsync(examId)).ReturnsAsync(exam);

        // Act
        await examService.DeleteExaminationAsync(examId);

        // Assert
        examinationRepositoryMock.Verify(msr => msr.DeleteExaminationAsync(exam), Times.Once);
    }

    [Fact]
    public async Task GetAllExaminationsAsync_ReturnsEmptyList_WhenExamExist()
    {
        // Arrange
        var examRepositoryMock = new Mock<IExaminationRepository>();
        var examinationCacheRepositoryMock = new Mock<IExaminationCacheRepository>();
        
        var examService = new ExaminationService(examRepositoryMock.Object, examinationCacheRepositoryMock.Object);
        
        examRepositoryMock.Setup(nr => nr.GetAllExaminationsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Examination>());
        
        // Act
        var result = await examService.GetAllExaminationsAsync(0, 10);

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task UpdateExamAsync_WhenExamDoesNotExist_ShouldThrowExaminationNotFoundException()
    {
        // Arrange
        var examinationRepositoryMock = new Mock<IExaminationRepository>();
        var examinationCacheRepositoryMock = new Mock<IExaminationCacheRepository>();
        var examId = 1;

        var examService = new ExaminationService(examinationRepositoryMock.Object, examinationCacheRepositoryMock.Object);
        
        var exam = new Examination { ExaminationId = examId };
        examinationRepositoryMock.Setup(msr => msr.GetExaminationByIdAsync(examId)).ReturnsAsync(exam);

        var examDto = new ExaminationDto
        {
            ExamId = 2,
            ExamName = "Badanie okresowe",
            ExamCategory = ExamCategory.Lab()
        };

        // Act & Assert
        await Assert.ThrowsAsync<ExaminationNotFoundException>(() => examService.UpdateExaminationAsync(examDto));
    }
}