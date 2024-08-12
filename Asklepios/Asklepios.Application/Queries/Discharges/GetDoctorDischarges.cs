using Asklepios.Application.Abstractions;
using Asklepios.Core.DTO.Patients;

namespace Asklepios.Application.Queries.Discharges;

public class GetDoctorDischarges : IQuery<IEnumerable<DischargeItemDto>>
{
    public Guid DoctorId { get; set; }
}