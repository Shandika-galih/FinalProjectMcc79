using API.Contracts;
using API.DTOs.LeaveRequest;
using API.Models;
using API.Repositories;
using System.Net.Mail;

namespace API.Services;

public class LeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailHandler _emailHandler;
    public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, ILeaveTypeRepository leaveTypeRepository, IAccountRepository accountRepository, IEmailHandler emailHandler)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _employeeRepository = employeeRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _accountRepository = accountRepository;
        _emailHandler = emailHandler;
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
        var employee = _employeeRepository.GetByGuid(newLeaveRequestDto.EmployeesGuid); 

        if (employee == null)
        {
            return null;
        }

        if (employee.EligibleLeave == 0)
        {
            return null;
        }

        /* var totalLeaveDays = Convert.ToInt32((newLeaveRequestDto.EndDate - newLeaveRequestDto.StartDate).TotalDays) + 1; ;

         if (employee.EligibleLeave - totalLeaveDays < 0)
         {
             // Employee cannot create a leave request due to insufficient remaining leave days.
             return null;
         }

         // Update the employee's EligibleLeave based on the leave request
         employee.EligibleLeave -= totalLeaveDays;
         _employeeRepository.Update(employee);*/

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

        /*// Kirim email notifikasi ke manager jika ditemukan
        var manager = _accountRepository.GetByGuid((Guid)employee.ManagerGuid);
        if (manager != null)
        {
            _emailHandler.SendEmail(manager.Email,
                "Leave Request Notification",
                $"Dear Manager,\n\nA new leave request has been submitted by {employee.FirstName} {employee.LastName} on {leaveRequest.SubmitDate}. The leave request starts on {leaveRequest.StartDate.ToShortDateString()} and ends on {leaveRequest.EndDate.ToShortDateString()}. Please review and take appropriate action.\n\nBest regards,\nYour Company");
        }*/

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
                          Guid = leaveRequest.Guid,
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


    public IEnumerable<GetEmployeeRequestDto> GetLeaveRequestsByManagerGuid(Guid managerGuid)
    {
        var employeesUnderManager = _employeeRepository.GetEmployeesByManagerGuid(managerGuid);

        var leaveRequests =
            from employee in _employeeRepository.GetEmployeesByManagerGuid(managerGuid)
            join leaveRequest in _leaveRequestRepository.GetAll() on employee.Guid equals leaveRequest.EmployeesGuid
            join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
            where employee.ManagerGuid == managerGuid &&
                 (leaveRequest.Status == Utilities.Enums.StatusEnum.Pending)
            select new GetEmployeeRequestDto
            {
                Guid = leaveRequest.Guid,
                NIK = employee.NIK,
                FullName = $"{employee.FirstName} {employee.LastName}",
                LeaveName = leaveType.LeaveName,
                Remarks = leaveRequest.Remarks,
                Attachment = leaveRequest.Attachment,
                SubmitDate = leaveRequest.SubmitDate,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Status = leaveRequest.Status,
            };

        return leaveRequests;
    }
}

