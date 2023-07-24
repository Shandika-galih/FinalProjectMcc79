using API.Utilities.Enums;

namespace API.DTOs.LeaveRequest;

public class NewLeaveRequestDto
{
    public StatusEnum Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime SubmitDate { get; set; }
    public string Remarks { get; set; }
    public byte[]? Attachment { get; set; }
    public Guid LeaveTypesGuid { get; set; }
    public Guid EmployeesGuid { get; set; }
}
