using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Employee;
using Client.ViewModels.LeaveRequest;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Client.Repositories;

public class ManagerRepository : GeneralRepository<ManagerVM, Guid>, IManagerRepository
{
    public ManagerRepository(string request = "manager/") : base(request)
    {
    }
    
}
