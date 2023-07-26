using API.Models;

namespace API.Contracts;

public interface ILeaveRequestRepository : IGeneralRepository<LeaveRequest>
{
    IEnumerable<LeaveRequest> GetLeaveRequestsByEmployeeGuid(Guid employeeGuid);
}
