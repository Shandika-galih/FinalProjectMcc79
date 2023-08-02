using API.Utilities.Enums;

namespace Client.ViewModels.LeaveRequest
{
    public class UpdateStatusRequestVM
    {
        public Guid Guid { get; set; }
        public StatusEnum Status { get; set; }
    }
}
