using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Policies.Users;
using Asklepios.Core.Repositories.Users;

namespace Asklepios.Application.Services.Users;

public class MedicalStaffService : IMedicalStaffService
{
    private readonly IMedicalStaffRepository _medicalStaffRepository;
    private readonly IRolePolicy _rolePolicy;

    public MedicalStaffService(IMedicalStaffRepository medicalStaffRepository, IRolePolicy rolePolicy)
    {
        _medicalStaffRepository = medicalStaffRepository;
        _rolePolicy = rolePolicy;
    }

    public async Task AddDoctorAsync(MedicalStaffDto dto)
    {
        if (await _rolePolicy.CannotCreateDoctor(dto.UserId) is false)
        {
            throw new CannotCreateDoctorException();
        }
        
        dto.DoctorId = Guid.NewGuid();
        await _medicalStaffRepository.AddDoctorAsync(new MedicalStaff
        {
            DoctorId = dto.DoctorId,
            Name = dto.Name,
            Surname = dto.Surname,
            PrivatePhoneNumber = dto.PrivatePhoneNumber,
            HospitalPhoneNumber = dto.HospitalPhoneNumber,
            Specialization = dto.Specialization,
            MedicalLicenseNumber = dto.MedicalLicenseNumber,
            UserId = dto.UserId,
            DepartmentId = dto.DepartmentId
        });
    }

    public async Task<MedicalStaffDto> GetDoctorAsync(Guid id)
    {
        var doctor = await _medicalStaffRepository.GetDoctorByIdAsync(id);
        if (doctor is null)
        {
            throw new DoctorNotFoundException(id);
        }
        
        var dto = Map<MedicalStaffDto>(doctor);
        return dto;
    }

    public async Task<Guid> GetDoctorIdAsync(Guid id)
    {

        var doctorId = await _medicalStaffRepository.GetDoctorIdAsync(id);
        return doctorId;
    }

    public async Task<IReadOnlyList<MedicalStaffListDto>> GetAllDoctorsAsync(int pageIndex, int pageSize)
    {
        var doctors = await _medicalStaffRepository.GetAllNDoctorsAsync(pageIndex, pageSize);
        return doctors.Select(MapMedicalStaffList<MedicalStaffListDto>).ToList();
    }

    public async Task UpdateDoctorAsync(MedicalStaffDto dto)
    {
        var doctor = await _medicalStaffRepository.GetDoctorByIdAsync(dto.DoctorId);
        if (doctor is null)
        {
            throw new DoctorNotFoundException(dto.DoctorId);
        }

        doctor.Name = dto.Name;
        doctor.Surname = dto.Surname;
        doctor.PrivatePhoneNumber = dto.PrivatePhoneNumber;
        doctor.HospitalPhoneNumber = dto.HospitalPhoneNumber;
        doctor.Specialization = dto.Specialization;
        doctor.MedicalLicenseNumber = dto.MedicalLicenseNumber;

        await _medicalStaffRepository.UpdateDoctorAsync(doctor);
    }

    public async Task DeleteDoctorAsync(Guid id)
    {
        var doctor = await _medicalStaffRepository.GetDoctorByIdAsync(id);
        if (doctor is null)
        {
            throw new DoctorNotFoundException(id);
        }

        await _medicalStaffRepository.DeleteDoctorAsync(doctor);
    }
    
    private static T Map<T>(MedicalStaff medicalStaff) where T : MedicalStaffDto, new() => new T()
    {
        DoctorId = medicalStaff.DoctorId,
        Name = medicalStaff.Name,
        Surname = medicalStaff.Surname,
        PrivatePhoneNumber = medicalStaff.PrivatePhoneNumber,
        HospitalPhoneNumber = medicalStaff.HospitalPhoneNumber,
        Specialization = medicalStaff.Specialization,
        MedicalLicenseNumber = medicalStaff.MedicalLicenseNumber,
        DepartmentId = medicalStaff.DepartmentId,
        UserId = medicalStaff.UserId
    };
    
    private static T MapMedicalStaffList<T>(MedicalStaff medicalStaff) where T : MedicalStaffListDto, new() => new T()
    {
        DoctorId = medicalStaff.DoctorId,
        Name = medicalStaff.Name,
        Surname = medicalStaff.Surname,
        PrivatePhoneNumber = medicalStaff.PrivatePhoneNumber,
        HospitalPhoneNumber = medicalStaff.HospitalPhoneNumber,
        Specialization = medicalStaff.Specialization,
        MedicalLicenseNumber = medicalStaff.MedicalLicenseNumber,
        DepartmentName = medicalStaff.Department.DepartmentName,
    };
}