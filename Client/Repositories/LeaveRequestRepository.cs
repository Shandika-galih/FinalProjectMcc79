using API.Utilities;
using Client.Contract;
using Client.Repository;
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

	public async Task<ResponseHandler<LeaveRequestVM>> Approval(Guid guid, LeaveRequestVM entity)
	{
		ResponseHandler<LeaveRequestVM> entityVM = null;
		StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
		using (var response = httpClient.PutAsync(request + "status" + "?guid=" + guid, content).Result)
		{
			string apiResponse = await response.Content.ReadAsStringAsync();
			entityVM = JsonConvert.DeserializeObject<ResponseHandler<LeaveRequestVM>>(apiResponse);
		}
		return entityVM;
	}
}
