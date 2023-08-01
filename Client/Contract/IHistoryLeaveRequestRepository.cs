using API.Utilities;
using Client.ViewModels.LeaveHistory;
using Client.ViewModels.LeaveRequest;

namespace Client.Contract;

public interface IHistoryLeaveRequestRepository
{
    Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistory();
    Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistoryApprove();
    Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistoryPending();
    Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistoryReject();
    Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistorybyNik();
    Task<ResponseHandler<IEnumerable<HistoryVM>>> GetLeaveHistorybyManager();
}
