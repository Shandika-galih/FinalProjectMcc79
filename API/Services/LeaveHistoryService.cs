using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.LeaveHistory;
using API.Models;
using API.Repositories;

namespace API.Services;

public class LeaveHistoryService
{
    private readonly ILeaveHistoryRepository _leaveHistoryRepository;

    public LeaveHistoryService(ILeaveHistoryRepository leaveHistoryRepository)
    {
        _leaveHistoryRepository = leaveHistoryRepository;
    }

    public IEnumerable<GetLeaveHistoryDto>? GetLeaveHistory()
    {
        var leaveHistory = _leaveHistoryRepository.GetAll();
        if (!leaveHistory.Any())
        {
            return null; // No Account Role found
        }

        var toDto = leaveHistory.Select(leaveHistory =>
                                            new GetLeaveHistoryDto
                                            {
                                                Guid = leaveHistory.Guid,
                                                LeaveRequestGuid = leaveHistory.LeaveRequestGuid
                                               
                                                
                                            }).ToList();

        return toDto; // Account Role found
    }

    public GetLeaveHistoryDto? getLeaveHistroy(Guid guid)
    {
        var leaveHistory = _leaveHistoryRepository.GetByGuid(guid);
        if (leaveHistory is null)
        {
            return null; // accountRole not found
        }

        var toDto = new GetLeaveHistoryDto
        {
            Guid = leaveHistory.Guid
        };

        return toDto; // Universities found
    }

    public GetLeaveHistoryDto? CreateLeaveHistory(NewLeaveHistoryDto newLeaveHistoryDto)
    {
        var leaveHistory = new LeaveHistory
        {
            Guid = new Guid(),
        };

        var createdLeaveHistory = _leaveHistoryRepository.Create(leaveHistory);
        if (createdLeaveHistory is null)
        {
            return null; // employee not created
        }

        var toDto = new GetLeaveHistoryDto
        {
            Guid = leaveHistory.Guid,
        };

        return toDto; // employee created
    }


    public int UpdateLeaveHistory(UpdateLeaveHistoryDto updateLeaveHistory)
    {
        var isExist = _leaveHistoryRepository.IsExist(updateLeaveHistory.Guid);
        if (!isExist)
        {
            return -1; // Account Role not found
        }

        var getleaveHistory = _leaveHistoryRepository.GetByGuid(updateLeaveHistory.Guid);

        var leaveHistory = new LeaveHistory
        {
            Guid = updateLeaveHistory.Guid,
            LeaveRequestGuid = updateLeaveHistory.LeaveRequestGuid

        };
        var isUpdate = _leaveHistoryRepository.Update(leaveHistory);
        if (!isUpdate)
        {
            return 0; // Account Role not updated
        }

        return 1;
    }

    public int DeleteleaveHistory(Guid guid)
    {
        var isExist = _leaveHistoryRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // Account Role not found
        }

        var leaveHistory = _leaveHistoryRepository.GetByGuid(guid);
        var isDelete = _leaveHistoryRepository.Delete(leaveHistory!);
        if (!isDelete)
        {
            return 0; // Account Role not deleted
        }

        return 1;
    }
}
