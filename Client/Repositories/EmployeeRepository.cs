using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Employee;
using Newtonsoft.Json;
using System.Net.Http;

namespace Client.Repositories;

public class EmployeeRepository : GeneralRepository<EmployeeVM, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "employees/") : base(request)
    {
    }

    public async Task<ResponseHandler<IEnumerable<EmployeeVM>>> GetEmployees()
    {
        ResponseHandler<IEnumerable<EmployeeVM>> entityDto = null;
        using (var response = await _httpClient.GetAsync(_request + "get-data-employee"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeVM>>>(apiResponse);
        }
        return entityDto;
    }

}