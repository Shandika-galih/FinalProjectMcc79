
using API.Utilities.Enums;

namespace Client.ViewModels.LeaveRequest
{
	public class UpdateStatusRequestVM
	{
        public Guid Guid { get; set; }
        public int NIK { get; set; }
        public string FullName { get; set; }
        public string LeaveName { get; set; }
        public string Remarks { get; set; }
        public string? Attachment { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusEnum Status { get; set; }
    }
}
