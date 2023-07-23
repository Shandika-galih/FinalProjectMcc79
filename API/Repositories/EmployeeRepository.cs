using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(MyDbContext context) : base(context)
    {
    }
}
