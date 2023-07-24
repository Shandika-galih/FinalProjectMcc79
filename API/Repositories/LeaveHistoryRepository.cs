using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class LeaveHistoryRepository : GeneralRepository<LeaveHistory>, ILeaveHistoryRepository
{
    public LeaveHistoryRepository(MyDbContext context) : base(context)
    {

    }
}
