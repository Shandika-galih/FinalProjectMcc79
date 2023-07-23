namespace API.DTOs.LeaveHistory
{
    public class GetLeaveHistoryDto
    {
        public Guid Guid { get; set; }
        public Guid LeaveRequestGuid { get; set; }
    }
}
