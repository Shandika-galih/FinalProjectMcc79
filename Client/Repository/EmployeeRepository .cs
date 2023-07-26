using API.DTOs.Employees;
using Client.Contract;

namespace Client.Repository;

public class EmployeeRepository : GeneralRepository<GetDataEmployeeDto, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "employees/") : base(request)
    {

        
    }
}