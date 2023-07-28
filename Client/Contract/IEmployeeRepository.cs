using API.DTOs.Employees;
using API.Utilities;
using Client.ViewModels.Employee;

namespace Client.Contract;

public interface IEmployeeRepository : IGeneralRepository<EmployeeVM, Guid>
{
    Task<ResponseHandler<IEnumerable<EmployeeVM>>> GetEmployees();
    Task<ResponseHandler<EmployeeVM>> GetEmployee(Guid guid);
}