using Asklepios.Application.Abstractions;
using Asklepios.Core.DTO.Patients;

namespace Asklepios.Application.Queries.Discharges;

public class GetDischargeById : IQuery<DischargeItemDto>
{
    public Guid DischargeId { get; set; }
}