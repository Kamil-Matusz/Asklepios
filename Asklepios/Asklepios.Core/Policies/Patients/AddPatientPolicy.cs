using Asklepios.Core.Repositories.Departments;

namespace Asklepios.Core.Policies.Patients;

public class AddPatientPolicy : IAddPatientPolicy
{
    private readonly IDepartmentRepository _departmentRepository;

    public AddPatientPolicy(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
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
}