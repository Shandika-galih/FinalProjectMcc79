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
        /*httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJOSUsiOiIxMTExMTciLCJGdWxsTmFtZSI6ImthdGluYSBub29iIiwiRW1haWwiOiJrYXJpbmFub29iOUBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNjkwNzA4Njg2LCJpc3MiOiJVcmxJc3N1ZXIiLCJhdWQiOiJVcmxBdWRpZW5jZSJ9.M6P8HFfwLz-cVfTnmMRsIW0xvpirDnhGmk1CetP5Rno");*/

        using (var response = await httpClient.GetAsync(request + "history"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<LeaveHistoryEmployeeVM>>>(apiResponse);
        }
        return entity;
    }

}
