using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Employee;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;

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

    public async Task<ResponseHandler<EmployeeVM>> GetEmployee(Guid guid)
    {
        ResponseHandler<EmployeeVM> entity = null;
        using (var response = await _httpClient.GetAsync(_request + "get-data-employee" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<EmployeeVM>>(apiResponse);
        }
        return entity;
    }


}