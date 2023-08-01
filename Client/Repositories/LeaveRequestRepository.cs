using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.LeaveRequest;
using Newtonsoft.Json;

namespace Client.Repositories;

public class LeaveRequestRepository : GeneralRepository<LeaveRequestVM, Guid>, ILeaveRequestRepository
{
	public LeaveRequestRepository(string request = "LeaveRequests/") : base(request)
	{
	}

    public async Task<ResponseHandler<IEnumerable<LeaveRequestVM>>> GetByManager(Guid guid)
    {
        ResponseHandler<IEnumerable<LeaveRequestVM>> entity = null;
        using (var response = await httpClient.GetAsync(request + "manager/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<LeaveRequestVM>>>(apiResponse);
        }
        return entity;
    }
}
