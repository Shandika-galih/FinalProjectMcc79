
using API.Utilities.Enums;

namespace Client.ViewModels.LeaveRequest
{
	public class LeaveRequestVM
	{
		public Guid Guid { get; set; }
		public int NIK { get; set; }
		public string FullName { get; set; }
		public string Manager { get; set; }
		public int EligibleLeave { get; set; }
		public string LeaveName { get; set; }
        public string Remarks { get; set; }
        public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime SubmitDate { get; set; }
		public string? Attachment { get; set; }
		public StatusEnum Status { get; set; }
		public Guid LeaveTypesGuid { get; set; }
		public Guid EmployeesGuid { get; set; }
        public Guid ManagerGuid { get; set; }
		public string? attachmentBase64 { get; set; }
    }
}
