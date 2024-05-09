namespace Asklepios.Core.Policies.Patients;

public interface IAddPatientPolicy
{
    Task<bool> CannotAddPatientToTheDepartment(Guid departmentId);
}