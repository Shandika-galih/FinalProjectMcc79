using Client.Contract;
using Client.Repository;
using Client.ViewModels.Employee;

namespace Client.Repositories;

public class EmployeeRepository : GeneralRepository<EmployeeVM, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "employees/") : base(request)
    {
    }
}