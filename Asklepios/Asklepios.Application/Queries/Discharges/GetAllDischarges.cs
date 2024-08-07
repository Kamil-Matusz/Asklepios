using Asklepios.Application.Abstractions;
using Asklepios.Core.DTO.Patients;

namespace Asklepios.Application.Queries.Discharges;

public class GetAllDischarges : IQuery<IEnumerable<DischargeItemDto>>
{
}