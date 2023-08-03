using API.Utilities;
using Client.ViewModels.Employee;
using Client.ViewModels.LeaveRequest;

namespace Client.Contract
{
	public interface ILeaveRequestRepository : IGeneralRepository<LeaveRequestVM, Guid>
	{
        Task<ResponseHandler<IEnumerable<LeaveRequestVM>>> GetByManager();
        Task<ResponseHandler<string>> ApproveStatus(UpdateStatusRequestVM leaveRequestFix);

    }
}
