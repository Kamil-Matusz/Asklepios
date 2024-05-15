using Asklepios.Application.Abstractions;
using Asklepios.Core.DTO.Patients;

namespace Asklepios.Application.Queries.Discharges;

public class GetDischargeByPesel : IQuery<DischargeItemDto>
{
    public string PeselNumber { get; set; }
}