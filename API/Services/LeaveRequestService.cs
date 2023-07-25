using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.LeaveRequest;
using API.Models;
using API.Repositories;

namespace API.Services;

public class LeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, ILeaveTypeRepository leaveTypeRepository)
    { 
        _leaveRequestRepository = leaveRequestRepository;
        _employeeRepository = employeeRepository;
        _leaveTypeRepository = leaveTypeRepository;
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
                                                  Attachment = leaveRequest.Attachment,
                                                  SubmitDate = leaveRequest.SubmitDate,
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
            Attachment = leaveRequest.Attachment,
            SubmitDate = leaveRequest.SubmitDate,
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
            SubmitDate = DateTime.Now,
            StartDate = newLeaveRequestDto.StartDate,
            EndDate = newLeaveRequestDto.EndDate,
            Remarks = newLeaveRequestDto.Remarks,
            Attachment = newLeaveRequestDto.Attachment ?? null,
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
            SubmitDate = leaveRequest.SubmitDate,
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            Remarks = leaveRequest.Remarks,
            Attachment = leaveRequest.Attachment ?? null,
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

    public IEnumerable<GetEmployeeRequestDto> GetEmployeeRequest()
    {
        var data = (from leaveRequest in _leaveRequestRepository.GetAll()
                      join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid equals employee.Guid join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
                      select new GetEmployeeRequestDto
                      {
                          Guid = employee.Guid,
                          NIK = employee.NIK,
                          FullName = employee.FirstName + " " + employee.LastName,
                          LeaveName = leaveType.LeaveName,
                          Remarks = leaveRequest.Remarks,
                          SubmitDate = DateTime.Now,
                          StartDate = leaveRequest.StartDate,
                          EndDate = leaveRequest.EndDate,
                          Status = leaveRequest.Status
                      }).ToList();
        if (!data.Any())
        {
            return null;
        }
        return data;
    }
}

