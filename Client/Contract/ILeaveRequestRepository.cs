using API.Utilities;
using Client.ViewModels.Employee;
using Client.ViewModels.LeaveRequest;

namespace Client.Contract
{
	public interface ILeaveRequestRepository : IGeneralRepository<LeaveRequestVM, Guid>
	{
        Task<ResponseHandler<IEnumerable<LeaveRequestVM>>> GetByManager(Guid guid);
        Task<ResponseHandler<string>> ApproveStatus(UpdateStatusRequestVM updateStatus);

    }
}
