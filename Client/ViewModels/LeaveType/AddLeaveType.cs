using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.LeaveType;

public class AddLeaveType
{
    [Required]
    public string LeaveName { get; set; }
    [Required]
    public int LeaveDay { get; set; }
    [Required]
    public string LeaveDescription { get; set; }
}
