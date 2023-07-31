using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.LeaveHistory;
using Client.ViewModels.LeaveRequest;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

namespace Client.Repositories;

public class HistoryLeaveRequestRepository : GeneralRepository<HistoryVM, Guid>, IHistoryLeaveRequestRepository
{
    public HistoryLeaveRequestRepository(string request = "HistoryLeaveRequest/") : base(request)
    {

    }

    public async Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistory()
    {
        ResponseHandler<IEnumerable<HistoryVM>> entity = null;

        using (var response = await httpClient.GetAsync(request))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<HistoryVM>>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistoryApprove()
    {
        ResponseHandler<IEnumerable<HistoryVM>> entity = null;
        using (var response = await httpClient.GetAsync(request + "AllHistoryApprove"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<HistoryVM>>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistorybyNik()
    {
        ResponseHandler<IEnumerable<HistoryVM>> entity = null;

        using (var response = await httpClient.GetAsync(request + "byNik"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<HistoryVM>>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistoryPending()
    {
        ResponseHandler<IEnumerable<HistoryVM>> entity = null;
        using (var response = await httpClient.GetAsync(request + "AllHistoryPending"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<HistoryVM>>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistoryReject()
    {
        ResponseHandler<IEnumerable<HistoryVM>> entity = null;
        using (var response = await httpClient.GetAsync(request + "AllHistoryReject"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<HistoryVM>>>(apiResponse);
        }
        return entity;
    }
}
