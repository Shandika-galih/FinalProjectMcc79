using System.ComponentModel.DataAnnotations;

namespace API.DTOs.LeaveType
{
    public class UpdateLeaveTypeDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string? LeaveName { get; set; }
        [Required]
        public string? LeaveDescription { get; set; }
    }
}
