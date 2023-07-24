using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class LeaveTypeRepository : GeneralRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(MyDbContext context) : base(context)
    {
    }
}
