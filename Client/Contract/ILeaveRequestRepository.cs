using API.Utilities;
using Client.ViewModels.LeaveRequest;

namespace Client.Contract
{
	public interface ILeaveRequestRepository : IGeneralRepository<LeaveRequestVM, Guid>
	{
        Task<ResponseHandler<IEnumerable<LeaveRequestVM>>> GetByManager();
        Task<ResponseHandler<LeaveRequestVM>> Approval(Guid guid, LeaveRequestVM entity);


    }
}
