using API.DTOs.Employees;

namespace Client.Contract;

public interface IEmployeeRepository : IRepository<GetDataEmployeeDto, Guid>
{

}