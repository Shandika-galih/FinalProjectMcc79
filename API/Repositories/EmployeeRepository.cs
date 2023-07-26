using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(MyDbContext context) : base(context)
    {
    }
    public bool IsDuplicateValue(string value)
    {
        return _context.Set<Employee>()
                       .FirstOrDefault(e => e.PhoneNumber.Contains(value)) is null;
    }

    public IEnumerable<Employee> GetEmployeesByManagerGuid(Guid managerGuid)
    {
        return _context.Employees.Where(e => e.ManagerGuid == managerGuid).ToList();
    }
}
