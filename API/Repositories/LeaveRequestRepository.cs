using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class LeaveRequestRepository : GeneralRepository<LeaveRequest>, ILeaveRequest
{
    public LeaveRequestRepository(MyDbContext context) : base(context)
    {

    }
}
