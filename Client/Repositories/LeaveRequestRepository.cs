using Client.Contract;
using Client.Repository;
using Client.ViewModels.LeaveRequest;

namespace Client.Repositories
{
	public class LeaveRequestRepository : GeneralRepository<LeaveRequestVM, Guid>, ILeaveRequestRepository
	{
		public LeaveRequestRepository(string request = "LeaveRequests/") : base(request)
		{
		}
	}
}
