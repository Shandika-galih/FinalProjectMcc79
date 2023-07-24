using System.ComponentModel.DataAnnotations;

namespace API.DTOs.LeaveHistory
{
    public class UpdateLeaveHistoryDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public Guid LeaveRequestGuid { get; set; }
    }
}
