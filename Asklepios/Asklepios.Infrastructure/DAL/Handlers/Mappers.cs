using Asklepios.Core.DTO.Patients;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Entities.Users;

namespace Asklepios.Infrastructure.DAL.Handlers;

public static class Mappers
{
    public static AccountDto AsAccountDto(this User entity)
        => new()
        {
            UserId = entity.UserId,
            Email = entity.Email,
            Role = entity.Role,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };
    
    public static DischargeItemDto AsDischargeItemDto(this Discharge entity)
        => new()
        {
            DischargeId = entity.DischargeId,
            PatientName = entity.PatientName,
            PatientSurname = entity.PatientSurname,
            PeselNumber = entity.PeselNumber,
            Address = entity.Address,
            Date = entity.Date,
            DischargeReasson = entity.DischargeReasson,
            Summary = entity.Summary,
            DoctorName = entity.MedicalStaff.Name,
            DoctorSurname = entity.MedicalStaff.Surname,
        };
    
    public static UserDto AsUsersDto(this User entity)
    {
        return new UserDto
        {
            UserId = entity.UserId,
            Email = entity.Email,
            Role = entity.Role,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };
    }
}