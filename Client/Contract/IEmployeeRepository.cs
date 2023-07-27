using API.DTOs.Employees;
using Client.ViewModels.Employee;

namespace Client.Contract;

public interface IEmployeeRepository : IGeneralRepository<EmployeeVM, Guid>
{

}