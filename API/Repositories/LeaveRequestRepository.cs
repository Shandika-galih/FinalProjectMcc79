using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class LeaveRequestRepository : GeneralRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(MyDbContext context) : base(context)
    {

    }

    public IEnumerable<LeaveRequest> GetLeaveRequestsByEmployeeGuid(Guid employeeGuid)
    {
        return _context.LeaveRequests.Where(lr => lr.EmployeesGuid == employeeGuid).ToList();
    }
}
