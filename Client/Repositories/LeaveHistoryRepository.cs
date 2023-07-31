using API.Utilities;
using Azure.Core;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Account;
using Client.ViewModels.Employee;
using Client.ViewModels.LeaveHistory;
using Client.ViewModels.Role;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Repositories;

public class LeaveHistoryRepository : GeneralRepository<LeaveHistoryEmployeeVM, Guid>, ILeaveHistoryRepository
{
    public LeaveHistoryRepository(string request = "leave_history/") : base(request)
    {
    }
    public async Task<ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>>> GetLeaveHistory()
    {
        ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>> entity = null;
        
        using (var response = await httpClient.GetAsync(request + "GetAllLeaveHistory"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>>>(apiResponse);
        }
        return entity;
    }
    public async Task<ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>>> GetLeaveHistoryEmployee()
    {
        ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>> entity = null;


        using (var response = await httpClient.GetAsync(request + "history"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>>>(apiResponse);
        }
        return entity;
    }

}
