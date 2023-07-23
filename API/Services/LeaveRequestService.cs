using API.Contracts;
using API.DTOs.LeaveRequest;
using API.Models;

namespace API.Services;

public class LeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository)
    { 
        _leaveRequestRepository = leaveRequestRepository;
    }
    public IEnumerable<GetLeaveRequestDto>? GetLeaveRequest()
    {
        var leaveRequests = _leaveRequestRepository.GetAll();
        if (!leaveRequests.Any()) 
        {
            return null;
        }
        
        var toDto = leaveRequests.Select(leaveRequest => 
                                              new GetLeaveRequestDto
                                              {
                                                  Guid = leaveRequest.Guid,
                                                  Status = leaveRequest.Status,
                                                  StartDate = leaveRequest.StartDate,
                                                  EndDate = leaveRequest.EndDate,
                                                  Remarks = leaveRequest.Remarks,
                                                  EligibleLeave = leaveRequest.EligibleLeave,
                                                  TotalLeave = leaveRequest.TotalLeave,
                                                  Attachment = leaveRequest.Attachment,
                                                  LeaveTypesGuid = leaveRequest.LeaveTypesGuid,
                                                  EmployeesGuid = leaveRequest.EmployeesGuid,
                                              }).ToList();
        return toDto; // Leave Request Found
    }

    public GetLeaveRequestDto? GetLeaveRequest(Guid guid)
    {
        var leaveRequest = _leaveRequestRepository.GetByGuid(guid);
        if (leaveRequest is null)
        {
            return null; //Leave Request not found
        }
        var toDto = new GetLeaveRequestDto
        {
            Guid = leaveRequest.Guid,
            Status = leaveRequest.Status,
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            Remarks = leaveRequest.Remarks,
            EligibleLeave = leaveRequest.EligibleLeave,
            TotalLeave = leaveRequest.TotalLeave,
            Attachment = leaveRequest.Attachment,
            LeaveTypesGuid = leaveRequest.LeaveTypesGuid,
            EmployeesGuid = leaveRequest.EmployeesGuid,
        };
        return toDto;
    }

    public GetLeaveRequestDto? CreateLeaveRequest(NewLeaveRequestDto newLeaveRequestDto)
    {
        var leaveRequest = new LeaveRequest
        {
            Guid = new Guid(),
            Status = newLeaveRequestDto.Status,
            StartDate = newLeaveRequestDto.StartDate,
            EndDate = newLeaveRequestDto.EndDate,
            Remarks = newLeaveRequestDto.Remarks,
            EligibleLeave = newLeaveRequestDto.EligibleLeave,
            TotalLeave = newLeaveRequestDto.TotalLeave,
            Attachment = newLeaveRequestDto.Attachment,
            LeaveTypesGuid = newLeaveRequestDto.LeaveTypesGuid,
            EmployeesGuid = newLeaveRequestDto.EmployeesGuid,
        };
        
        var createdLeaveRequest = _leaveRequestRepository.Create(leaveRequest);
        if (createdLeaveRequest is null)
        {
            return null;
        }
        var toDto = new GetLeaveRequestDto
        {
            Guid = leaveRequest.Guid,
            Status = leaveRequest.Status,
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            Remarks = leaveRequest.Remarks,
            EligibleLeave = leaveRequest.EligibleLeave,
            TotalLeave = leaveRequest.TotalLeave,
            Attachment = leaveRequest.Attachment,
            LeaveTypesGuid = leaveRequest.LeaveTypesGuid,
            EmployeesGuid = leaveRequest.EmployeesGuid,
        };
        return toDto;
    }

    public int UpdateLeaveRequest(UpdateLeaveRequestDto updateLeaveRequestDto)
    {
        var isExist = _leaveRequestRepository.IsExist(updateLeaveRequestDto.Guid);
        if (isExist)
        {
            return -1; 
        }

        var getLeaveRequest = _leaveRequestRepository.GetByGuid(updateLeaveRequestDto.Guid);

        var leaveRequest = new LeaveRequest
        {
            Guid = updateLeaveRequestDto.Guid,
            Status = updateLeaveRequestDto.Status,
            StartDate = updateLeaveRequestDto.StartDate,
            EndDate = updateLeaveRequestDto.EndDate,
            Remarks = updateLeaveRequestDto.Remarks,
            EligibleLeave = updateLeaveRequestDto.EligibleLeave,
            TotalLeave = updateLeaveRequestDto.TotalLeave,
            Attachment = updateLeaveRequestDto.Attachment,
            LeaveTypesGuid = updateLeaveRequestDto.LeaveTypesGuid,
            EmployeesGuid = updateLeaveRequestDto.EmployeesGuid,
        };

        var isUpdate = _leaveRequestRepository.Update(leaveRequest);
        if (!isUpdate)
        {
            return 0;
        }

        return 1;
    }

    public int DeleteLeaveRequest(Guid guid)
    {
        var isExist = _leaveRequestRepository.IsExist(guid);
        if (!isExist)
        {
            return -1;
        }
         var leaveRequest = _leaveRequestRepository.GetByGuid(guid);
        var isDelete = _leaveRequestRepository.Delete(leaveRequest!); ;
        if (!isDelete)
        {
            return 0;
        }

        return 1;
    }
}
