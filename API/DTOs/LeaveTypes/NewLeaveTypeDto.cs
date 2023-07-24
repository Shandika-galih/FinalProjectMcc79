using System.ComponentModel.DataAnnotations;

namespace API.DTOs.LeaveType
{
    public class NewLeaveTypeDto
    {
        [Required]
        public string? LeaveName { get; set; }
        [Required]
        public string? LeaveDescription { get; set; }
    }
}
