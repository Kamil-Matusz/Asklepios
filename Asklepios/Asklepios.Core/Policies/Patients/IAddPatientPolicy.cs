namespace Asklepios.Core.Policies.Patients;

public interface IAddPatientPolicy
{
    Task<bool> CannotAddPatientToTheDepartment(Guid departmentId);
    Task<bool> CannotAddPatientToTheRoom(Guid roomId);
}