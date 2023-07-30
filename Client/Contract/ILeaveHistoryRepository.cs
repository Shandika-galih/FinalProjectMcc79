using API.Utilities;
using Client.ViewModels.Employee;
using Client.ViewModels.LeaveHistory;

namespace Client.Contract;

public interface ILeaveHistoryRepository : IGeneralRepository<LeaveHistoryEmployeeVM, Guid>
{
    Task<ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>>> GetLeaveHistory();
    Task<ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>>> GetLeaveHistoryEmployee();
}
