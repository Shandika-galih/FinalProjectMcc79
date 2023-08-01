using API.Contracts;
using API.DTOs.LeaveRequest;

namespace API.Services;

public class HistoryLeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public HistoryLeaveRequestService( ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, IAccountRepository accountRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public IEnumerable<GetEmployeeRequestDto> GetHistoryEmployeeRequest()
    {
        var data = (from leaveRequest in _leaveRequestRepository.GetAll()
                    join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid equals employee.Guid
                    join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
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

    public IEnumerable<GetEmployeeRequestDto> GetHistoryPending()
    {
        var data = (from leaveRequest in _leaveRequestRepository.GetAll()
                    join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid equals employee.Guid
                    join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
                    where leaveRequest.Status == Utilities.Enums.StatusEnum.Pending
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

    public IEnumerable<GetEmployeeRequestDto> GetHistoryReject()
    {
        var data = (from leaveRequest in _leaveRequestRepository.GetAll()
                    join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid equals employee.Guid
                    join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
                    where leaveRequest.Status == Utilities.Enums.StatusEnum.Rejected
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

    public IEnumerable<GetEmployeeRequestDto> GetHistoryApprove()
    {
        var data = (from leaveRequest in _leaveRequestRepository.GetAll()
                    join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid equals employee.Guid
                    join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
                    where leaveRequest.Status == Utilities.Enums.StatusEnum.Approved
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

    public IEnumerable<GetEmployeeRequestDto> GetHistorybyNik(int nik)
    {
        var data = (from leaveRequest in _leaveRequestRepository.GetAll()
                    join employee in _employeeRepository.GetAll() on leaveRequest.EmployeesGuid equals employee.Guid
                    join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
                    where employee.NIK == nik
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

    public IEnumerable<GetEmployeeRequestDto> GetByGuidManager(Guid managerGuid)
    {
        var leaveRequests =
            from employee in _employeeRepository.GetEmployeesByManagerGuid(managerGuid)
            join leaveRequest in _leaveRequestRepository.GetAll() on employee.Guid equals leaveRequest.EmployeesGuid
            join leaveType in _leaveTypeRepository.GetAll() on leaveRequest.LeaveTypesGuid equals leaveType.Guid
            where employee.ManagerGuid == managerGuid &&
                  (leaveRequest.Status == Utilities.Enums.StatusEnum.Approved ||
                   leaveRequest.Status == Utilities.Enums.StatusEnum.Rejected)
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

}
