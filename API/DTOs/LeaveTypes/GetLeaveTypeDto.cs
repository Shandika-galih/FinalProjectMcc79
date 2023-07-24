namespace API.DTOs.LeaveType
{
    public class GetLeaveTypeDto
    {
        public Guid Guid { get; set; }
        public string? LeaveName { get; set; }
        public int LeaveDay { get; set; }
        public string? LeaveDescription { get; set; }
    }
}
