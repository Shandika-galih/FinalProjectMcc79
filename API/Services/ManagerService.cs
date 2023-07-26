using API.Contracts;
using API.DTOs.LeaveRequest;
using API.DTOs.Manager;
using API.DTOs.Role;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
using System.Data;

namespace API.Services;

public class ManagerService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public ManagerService(IEmployeeRepository employeeRepository, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        _employeeRepository = employeeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public IEnumerable<GetEmployeeRequestDto> GetLeaveRequestsByManagerGuid(Guid managerGuid)
    {
        var employeesUnderManager = _employeeRepository.GetEmployeesByManagerGuid(managerGuid);

        var leaveRequests =
            from employee in _employeeRepository.GetEmployeesByManagerGuid(managerGuid)
            join leaveRequest in _leaveRequestRepository.GetAll() on employee.Guid equals leaveRequest.EmployeesGuid join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
            select new GetEmployeeRequestDto
            {
                Guid = leaveRequest.Guid,
                NIK = employee.NIK,
                FullName = $"{employee.FirstName} {employee.LastName}",
                LeaveName = leaveType.LeaveName, 
                Remarks = leaveRequest.Remarks,
                SubmitDate = leaveRequest.SubmitDate,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Status = leaveRequest.Status,
            };

        return leaveRequests;
    }

  /*  public int UpdateLeaveRequestStatus(UpdateStatusRequestDto updateStatus)
    {
        var isExist = _leaveRequestRepository.IsExist(updateStatus.Guid);
        if (!isExist)
        {
            return -1; 
        }
        var leave = _leaveRequestRepository.GetByGuid(updateStatus.Guid);

        var leaveRequest = new LeaveRequest
        {
            Guid = updateStatus.Guid,
            Status = updateStatus.Status,
        };

        var isUpdate = _leaveRequestRepository.Update(leaveRequest);
        if (!isUpdate)
        {
            return 0; 
        }

        return 1;
    }*/

    public bool UpdateLeaveRequestStatus(UpdateStatusRequestDto updateStatus)
    {
        var leaveRequest = _leaveRequestRepository.GetByGuid(updateStatus.Guid);

        if (leaveRequest == null)
        {
            return false;
        }

        // Update the status of the LeaveRequest
        leaveRequest.Status = updateStatus.Status;

        // Save the updated LeaveRequest
        bool isUpdateSuccessful = _leaveRequestRepository.Update(leaveRequest);

        return isUpdateSuccessful;
    }

}
