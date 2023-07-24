using API.Contracts;
using API.DTOs.LeaveType;
using API.Models;



namespace API.Services;

public class LeaveTypeService 
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public IEnumerable<GetLeaveTypeDto>? GetLeaveType()
    {
        var leaveType = _leaveTypeRepository.GetAll();
        if (!leaveType.Any())
        {
            return null; // No Account Role found
        }

        var toDto = leaveType.Select(leaveType =>
                                            new GetLeaveTypeDto
                                            {
                                                Guid = leaveType.Guid,
                                               LeaveName = leaveType.LeaveName,
                                               LeaveDay = leaveType.LeaveDay,
                                               LeaveDescription = leaveType.LeaveDescription
                                            }).ToList();

        return toDto; // Account Role found
    }

    public GetLeaveTypeDto? getLeaveType(Guid guid)
    {
        var leaveType = _leaveTypeRepository.GetByGuid(guid);
        if (leaveType is null)
        {
            return null; // accountRole not found
        }

        var toDto = new GetLeaveTypeDto
        {
            Guid = leaveType.Guid,
            LeaveName = leaveType.LeaveName,
            LeaveDay = leaveType.LeaveDay,
            LeaveDescription = leaveType.LeaveDescription
        };

        return toDto; // Universities found
    }

    public GetLeaveTypeDto? CreateLeaveType(NewLeaveTypeDto newLeaveTypeDto)
    {
        var leaveType = new LeaveType
        {
            Guid = new Guid(),
            LeaveName = newLeaveTypeDto.LeaveName,
            LeaveDay = newLeaveTypeDto.LeaveDay,
            LeaveDescription= newLeaveTypeDto.LeaveDescription,
        };

        var createdLeaveType = _leaveTypeRepository.Create(leaveType);
        if (createdLeaveType is null)
        {
            return null; // employee not created
        }

        var toDto = new GetLeaveTypeDto
        {
            Guid = leaveType.Guid,
            LeaveName = leaveType.LeaveName,
            LeaveDay = leaveType.LeaveDay,
            LeaveDescription = leaveType.LeaveDescription,

        };

        return toDto; // employee created
    }


    public int UpdateleaveType(UpdateLeaveTypeDto updateleaveType)
    {
        var isExist = _leaveTypeRepository.IsExist(updateleaveType.Guid);
        if (!isExist)
        {
            return -1; // Account Role not found
        }

        var getleaveType = _leaveTypeRepository.GetByGuid(updateleaveType.Guid);

        var leaveType = new LeaveType
        {
            Guid = updateleaveType.Guid,
            LeaveName = updateleaveType.LeaveName,
            LeaveDay = updateleaveType.LeaveDay,
            LeaveDescription = updateleaveType.LeaveDescription

        };
            var isUpdate = _leaveTypeRepository.Update(leaveType);
        if (!isUpdate)
        {
            return 0; // Account Role not updated
        }

        return 1;
    }

    public int DeleteleaveType(Guid guid)
    {
        var isExist = _leaveTypeRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // Account Role not found
        }

        var leaveType = _leaveTypeRepository.GetByGuid(guid);
        var isDelete = _leaveTypeRepository.Delete(leaveType!);
        if (!isDelete)
        {
            return 0; // Account Role not deleted
        }

        return 1;
    }
}
