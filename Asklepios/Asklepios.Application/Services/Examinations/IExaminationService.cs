using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Entities.Examinations;

namespace Asklepios.Application.Services.Examinations;

public interface IExaminationService
{
    Task AddExaminationAsync(ExaminationDto dto);
    Task<ExaminationDto> GetExaminationAsync(int id);
    Task<IReadOnlyList<ExaminationDto>> GetAllExaminationsAsync(int pageIndex, int pageSize);
    Task<IReadOnlyList<ExaminationDto>> GetAllExaminationsByCategoryAsync(string category, int pageIndex, int pageSize);
    Task UpdateExaminationAsync(ExaminationDto dto);
    Task DeleteExaminationAsync(int id);
    Task<List<ExaminationAutocompleteDto>> GetExaminationsList();
}