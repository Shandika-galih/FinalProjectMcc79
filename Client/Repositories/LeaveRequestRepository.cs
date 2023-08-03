using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Account;
using Client.ViewModels.LeaveRequest;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositories;

public class LeaveRequestRepository : GeneralRepository<LeaveRequestVM, Guid>, ILeaveRequestRepository
{
	public LeaveRequestRepository(string request = "LeaveRequests/") : base(request)
	{
	}

    public async Task<ResponseHandler<IEnumerable<LeaveRequestVM>>> GetByManager()
    {
        ResponseHandler<IEnumerable<LeaveRequestVM>> entity = null;
        using (var response = await httpClient.GetAsync(request + "manager"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<LeaveRequestVM>>>(apiResponse);
		}
		return entity;
	}
    public async Task<ResponseHandler<string>> ApproveStatus(UpdateStatusRequestVM leaveRequestFix)
    {
        ResponseHandler<string> entity = null;
        string url = request + "status?guid=";
        StringContent content = new StringContent(JsonConvert.SerializeObject(leaveRequestFix), Encoding.UTF8, "application/json");
        using (var response = await httpClient.PutAsync(url, content))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }
        return entity;
    }
}
