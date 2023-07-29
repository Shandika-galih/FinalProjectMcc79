using API.Utilities;
using Client.ViewModels.Employee;

namespace Client.Contract
{
    public interface IManagerRepository : IGeneralRepository<ManagerVM, Guid>
    {
    }
}
