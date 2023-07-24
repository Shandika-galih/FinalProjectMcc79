using API.Utilities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.LeaveRequest;

public class GetLeaveRequestDto
{
    public Guid Guid { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime SubmitDate { get; set; }
    public string Remarks { get; set; }
    public byte[]? Attachment { get; set; }
    public Guid LeaveTypesGuid { get; set; }
    public Guid EmployeesGuid { get; set; }
}
