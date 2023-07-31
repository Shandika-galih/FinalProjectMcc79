using Client.Contract;
using Client.Repository;
using Client.ViewModels.Employee;
using Client.ViewModels.LeaveType;

namespace Client.Repositories
{
    public class LeaveTypeRepository : GeneralRepository<LeaveTypeVM, Guid>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(string request ="leave_type/") : base(request)
        {
        }
    }
}
