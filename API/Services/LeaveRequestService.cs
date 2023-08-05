using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.LeaveRequest;
using API.DTOs.Manager;
using API.Models;
using API.Repositories;
using System.Diagnostics.Metrics;
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

    public GetEmployeeRequestDto? GetLeaveRequest(Guid guid)
    {
        var data = GetEmployeeRequest();
        var dataByGuid = data.FirstOrDefault(data => data.Guid == guid);
        return dataByGuid;
    }


    public GetLeaveRequestDto? CreateLeaveRequest(NewLeaveRequestDto newLeaveRequestDto)
    {
        var employee = _employeeRepository.GetByGuid(newLeaveRequestDto.EmployeesGuid);


        if (employee == null)
        {
            return null;
        }
	

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

    public bool UpdateLeaveRequestStatus(UpdateStatusRequestDto updateStatus)
    {
        var leaveRequest = _leaveRequestRepository.GetByGuid(updateStatus.Guid);

        if (leaveRequest == null)
        {
            return false;
        }

        // Update the status of the LeaveRequest
        leaveRequest.Guid = updateStatus.Guid;
        leaveRequest.Status = updateStatus.Status;

        // Check if the status is Approved
        if (updateStatus.Status == Utilities.Enums.StatusEnum.Approved)
        {
            int workingDays = CalculateWorkingDays(leaveRequest.StartDate, leaveRequest.EndDate);

            var employee = _employeeRepository.GetByGuid(leaveRequest.EmployeesGuid);
            if (employee != null)
            {
                // Check if the LeaveType is "Cuti Normal"
                var leaveType = _leaveTypeRepository.GetByGuid(leaveRequest.LeaveTypesGuid);
                if (leaveType != null && leaveType.LeaveName == "Cuti Normal")
                {
                    employee.EligibleLeave -= workingDays;
                }

                _employeeRepository.Update(employee);
            }
        }

        // Save the updated LeaveRequest
        bool isUpdateSuccessful = _leaveRequestRepository.Update(leaveRequest);

        return isUpdateSuccessful;
    }


    private int CalculateWorkingDays(DateTime startDate, DateTime endDate)
    {
        int workingDays = 0;
        DateTime currentDate = startDate;

        while (currentDate <= endDate)
        {
            if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                workingDays++;
            }
            currentDate = currentDate.AddDays(1);
        }

        return workingDays;
    }

    public IEnumerable<GetEmployeeRequestDto> GetbyEmployeePending(int nik)
    {
        var data = (from leaveRequest in _leaveRequestRepository.GetAll()
                    join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid equals employee.Guid
                    join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
                    where employee.NIK == nik &&
                  (leaveRequest.Status == Utilities.Enums.StatusEnum.Pending)
                    select new GetEmployeeRequestDto
                    {
                        Guid = leaveRequest.Guid,
                        NIK = employee.NIK,
                        FullName = employee.FirstName + " " + employee.LastName,
                        LeaveName = leaveType.LeaveName,
                        Remarks = leaveRequest.Remarks,
                        Attachment = leaveRequest.Attachment,
                        SubmitDate = leaveRequest.SubmitDate,
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

