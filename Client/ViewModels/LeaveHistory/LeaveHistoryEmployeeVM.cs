using API.Utilities.Enums;

namespace Client.ViewModels.LeaveHistory;

public class LeaveHistoryEmployeeVM
{
    public Guid Guid { get; set; }
    public int NIK { get; set; }
    public string FullName { get; set; }
    public GenderEnum Gender { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime SubmitDate { get; set; }
    public string Remarks { get; set; }
    public string? Attachment { get; set; }
    public string LeaveName { get; set; }
    public int EligibleLeave { get; set; }
}
