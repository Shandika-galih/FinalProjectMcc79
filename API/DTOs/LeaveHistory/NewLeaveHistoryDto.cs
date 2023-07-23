using System.ComponentModel.DataAnnotations;

namespace API.DTOs.LeaveHistory
{
    public class NewLeaveHistoryDto
    {
        [Required]
        public Guid LeaveRequestGuid { get; set; }
    }
}
