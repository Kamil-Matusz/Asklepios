using Asklepios.Core.Repositories.Departments;

namespace Asklepios.Core.Policies.Patients;

public class AddPatientPolicy : IAddPatientPolicy
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IRoomRepository _roomRepository;

    public AddPatientPolicy(IDepartmentRepository departmentRepository, IRoomRepository roomRepository)
    {
        _departmentRepository = departmentRepository;
        _roomRepository = roomRepository;
    }

    public async Task<bool> CannotAddPatientToTheDepartment(Guid departmentId)
    {
        var numberOfBeds = await _departmentRepository.GetNumberOfBedsAsync(departmentId);
        var patientsCount = await _departmentRepository.CountPatientsInDepartmentAsync(departmentId);

        if (patientsCount < numberOfBeds)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> CannotAddPatientToTheRoom(Guid roomId)
    {
        var numberOfBeds = await _roomRepository.GetNumberOfBedsAsync(roomId);
        var patientsCount = await _roomRepository.CountPatientsInRoomAsync(roomId);

        if (patientsCount < numberOfBeds)
        {
            return true;
        }

        return false;
    }
}