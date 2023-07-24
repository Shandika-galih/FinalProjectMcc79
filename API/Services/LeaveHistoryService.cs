using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.LeaveHistory;
using API.Models;
using API.Repositories;
using System.Diagnostics.Metrics;
using System.Net.Mail;

namespace API.Services;

public class LeaveHistoryService
{
    private readonly ILeaveHistoryRepository _leaveHistoryRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public LeaveHistoryService(ILeaveHistoryRepository leaveHistoryRepository, ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, IAccountRepository accountRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveHistoryRepository = leaveHistoryRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
        _leaveTypeRepository = leaveTypeRepository;
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
            LeaveRequestGuid = newLeaveHistoryDto.LeaveRequestGuid,
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

    public IEnumerable<GetLeaveHistroyEmployeeDto> GetLeaveHistroyEmployees()
    {
        var data = (from leaveHistorie in _leaveHistoryRepository.GetAll()
                      join leaveRequest in _leaveRequestRepository.GetAll() on leaveHistorie.LeaveRequestGuid equals leaveRequest.Guid
                      join leaveTypes in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveTypes.Guid
                      join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid  equals employee.Guid
                      join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                      select new GetLeaveHistroyEmployeeDto
                      {
                          Guid = leaveHistorie.Guid,
                          Status = leaveRequest.Status,
                          StartDate = leaveRequest.StartDate,
                          EndDate = leaveRequest.EndDate,
                          SubmitDate = leaveRequest.SubmitDate,
                          Remarks = leaveRequest.Remarks,
                          Attachment = leaveRequest.Attachment,
                          FullName = employee.FirstName + " " + employee.LastName,
                          NIK = employee.NIK,
                          Email = account.Email,
                          PhoneNumber = employee.PhoneNumber,
                          Gender = employee.Gender,
                          EligibleLeave = employee.EligibleLeave,
                          LeaveName = leaveTypes.LeaveName
                      }).ToList();

        return data.Any() ? data : null;
    }

    public IEnumerable<GetLeaveHistroyEmployeeDto> GetLeaveHistroyEmployee(Guid employeeGuid)
    {
        var data = (from leaveHistorie in _leaveHistoryRepository.GetAll()
                    join leaveRequest in _leaveRequestRepository.GetAll() on leaveHistorie.LeaveRequestGuid equals leaveRequest.Guid
                    join leaveTypes in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveTypes.Guid
                    join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid equals employee.Guid
                    join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                    where employee.Guid == employeeGuid
                    select new GetLeaveHistroyEmployeeDto
                    {
                        // Properti lainnya
                        Guid = employee.Guid,
                        Status = leaveRequest.Status,
                        StartDate = leaveRequest.StartDate,
                        EndDate = leaveRequest.EndDate,
                        SubmitDate = leaveRequest.SubmitDate,
                        Remarks = leaveRequest.Remarks,
                        Attachment = leaveRequest.Attachment,
                        FullName = employee.FirstName + " " + employee.LastName,
                        NIK = employee.NIK,
                        Email = account.Email,
                        PhoneNumber = employee.PhoneNumber,
                        Gender = employee.Gender,
                        EligibleLeave = employee.EligibleLeave,
                        LeaveName = leaveTypes.LeaveName
                    }).ToList();

        return data; // Akan mengembalikan IEnumerable<GetLeaveHistroyEmployeeDto>.
    }


}
