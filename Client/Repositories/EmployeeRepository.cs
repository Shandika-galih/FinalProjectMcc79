using API.DTOs.Employees;
using Client.Contract;
using Client.Repository;

namespace Client.Repositories;

public class EmployeeRepository : GeneralRepository<GetEmployeeDto, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "employees/") : base(request)
    {
    }
}