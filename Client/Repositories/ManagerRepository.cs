using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Employee;
using Newtonsoft.Json;

namespace Client.Repositories;

public class ManagerRepository : GeneralRepository<ManagerVM, Guid>, IManagerRepository
{
    public ManagerRepository(string request = "manager/") : base(request)
    {
    }

}
