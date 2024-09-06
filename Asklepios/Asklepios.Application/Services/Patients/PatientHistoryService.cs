using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Repositories.Patients;

namespace Asklepios.Application.Services.Patients;

public class PatientHistoryService : IPatientHistoryService
{
    private readonly IPatientHistoryRepository _patientHistoryRepository;

    public PatientHistoryService(IPatientHistoryRepository patientHistoryRepository)
    {
        _patientHistoryRepository = patientHistoryRepository;
    }
    
    public async Task AddOrUpdatePatientHistoryAsync(PatientHistoryDto patientHistoryDto)
    {
        bool patientExists = await _patientHistoryRepository.PatientExistAsync(patientHistoryDto.PeselNumber);

        if (!patientExists)
        {
            var newPatientHistory = new PatientHistory
            {
                HistoryId = patientHistoryDto.HistoryId != Guid.Empty ? patientHistoryDto.HistoryId : Guid.NewGuid(),
                PatientName = patientHistoryDto.PatientName,
                PatientSurname = patientHistoryDto.PatientSurname,
                PeselNumber = patientHistoryDto.PeselNumber,
                History = SerializeHistoryToJson(patientHistoryDto.History)
            };

            await _patientHistoryRepository.AddPatientHistoryAsync(newPatientHistory);
        }
        else
        {
            var existingPatientHistory = await _patientHistoryRepository.GetFullPatientHistoryByPeselAsync(patientHistoryDto.PeselNumber);

            if (existingPatientHistory != null)
            {
                var existingHistoryList = DeserializeHistoryFromJson(existingPatientHistory.History);
                existingHistoryList.AddRange(patientHistoryDto.History); // Dodanie nowych wizyt do istniejącej historii

                existingPatientHistory.History = SerializeHistoryToJson(existingHistoryList); // Aktualizacja historii w bazie w formacie JSON

                await _patientHistoryRepository.UpdatePatientHistoryAsync(existingPatientHistory);
            }
        }
    }

    public async Task<PatientHistoryDto> GetPatientHistoryByIdAsync(Guid historyId)
    {
        var patientHistory = await _patientHistoryRepository.GetPatientHistoryByIdAsync(historyId);
        return MapToDto(patientHistory);
    }

    public async Task<PatientHistoryDto> GetFullPatientHistoryByPeselAsync(string peselNumber)
    {
        var patientHistory = await _patientHistoryRepository.GetFullPatientHistoryByPeselAsync(peselNumber);
        return MapToDto(patientHistory);
    }

    public async Task DeletePatientHistoryAsync(Guid historyId)
    {
        var history = await _patientHistoryRepository.GetPatientHistoryByIdAsync(historyId);
        if (history == null)
            throw new InvalidOperationException("Patient history not found.");

        await _patientHistoryRepository.DeletePatientHistoryAsync(history);
    }

    public async Task<bool> PatientExistAsync(string peselNumber)
    {
        return await _patientHistoryRepository.PatientExistAsync(peselNumber);
    }
    
    private string SerializeHistoryToJson(List<PatientVisitDto> history)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(history);
    }

    private List<PatientVisitDto> DeserializeHistoryFromJson(string historyJson)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<PatientVisitDto>>(historyJson) ?? new List<PatientVisitDto>();
    }
    
    private PatientHistoryDto MapToDto(PatientHistory patientHistory)
    {
        if (patientHistory == null) return null;

        return new PatientHistoryDto
        {
            HistoryId = patientHistory.HistoryId,
            PatientName = patientHistory.PatientName,
            PatientSurname = patientHistory.PatientSurname,
            PeselNumber = patientHistory.PeselNumber,
            History = DeserializeHistoryFromJson(patientHistory.History)
        };
    }
}