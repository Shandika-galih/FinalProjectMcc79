using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using API.Utilities;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRoleRepository _roleRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IRoleRepository roleRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
    }

    public IEnumerable<GetEmployeeDto>? GetEmployee()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return null; // No employee  found
        }
        var toDto = employees.Select(employee =>
                                           new GetEmployeeDto
                                           {
                                               Guid = employee.Guid,
                                               NIK = employee.NIK,
                                               FirstName = employee.FirstName,
                                               LastName = employee.LastName,
                                               Gender = employee.Gender,
                                               PhoneNumber = employee.PhoneNumber,
                                               ManagerId = employee.ManagerId
                                           }).ToList();
        return toDto; // employee found
    }

    public GetEmployeeDto? GetEmployee(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return null; // employee not found
        }

        var toDto = new GetEmployeeDto
        {
            Guid = employee.Guid,
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName ?? "",
            Gender = employee.Gender,
            PhoneNumber = employee.PhoneNumber,
            ManagerId = employee.ManagerId
        };

        return toDto; // employees found
    }

    public GetEmployeeDto? CreateEmployee(NewEmployeeDto newEmployeeDto)
    {
       

        var employee = new Employee
        {
            Guid = new Guid(),
            NIK = GenerateNik(),
            FirstName = newEmployeeDto.FirstName,
            LastName = newEmployeeDto.LastName,
            Gender = newEmployeeDto.Gender,
            PhoneNumber = newEmployeeDto.PhoneNumber,
            ManagerId = newEmployeeDto.ManagerId
        };

        var createdEmployee = _employeeRepository.Create(employee);
        if (createdEmployee is null)
        {
            return null; // employee not created
        }

        var toDto = new GetEmployeeDto
        {
            Guid = employee.Guid,
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = employee.Gender,
            PhoneNumber = employee.PhoneNumber,
            ManagerId = employee.ManagerId
        };

        return toDto; // employee created
    }

    public int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
    {
        var isExist = _employeeRepository.IsExist(updateEmployeeDto.Guid);
        if (!isExist)
        {
            return -1; // employee not found
        }

        var getEmployee = _employeeRepository.GetByGuid(updateEmployeeDto.Guid);

        var employee = new Employee
        {
            Guid = updateEmployeeDto.Guid,
            NIK = updateEmployeeDto.NIK,
            FirstName = updateEmployeeDto.FirstName,
            LastName = updateEmployeeDto.LastName,
            Gender = updateEmployeeDto.Gender,
            PhoneNumber = updateEmployeeDto.PhoneNumber,
            ManagerId = updateEmployeeDto.ManagerId
        };

        var isUpdate = _employeeRepository.Update(employee);
        if (!isUpdate)
        {
            return 0; // employee not updated
        }

        return 1;
    }
    public int DeleteEmployee(Guid guid)
    {
        var isExist = _employeeRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // employee not found
        }

        var employee = _employeeRepository.GetByGuid(guid);
        var isDelete = _employeeRepository.Delete(employee!);
        if (!isDelete)
        {
            return 0; // employee not deleted
        }

        return 1;
    }
    public int GenerateNik()
    {
        var employeeData = GetEmployee();
        if (employeeData is null)
        {
            return 111111;
        }

        GetEmployeeDto lastEmployee = employeeData.LastOrDefault();
        int newNik = lastEmployee.NIK + 1;

        return newNik;
    }

    public AddEmployeeDto? AddEmployee(AddEmployeeDto addEmployeeDto)
    {
       
        if (addEmployeeDto.Password != addEmployeeDto.ConfirmPassword)
        {
            return null;
        }
    
        Employee employee = new Employee
        {
            Guid = new Guid(),
            NIK = GenerateNik(),
            FirstName = addEmployeeDto.FirstName,
            LastName = addEmployeeDto.LastName,
            Gender = addEmployeeDto.Gender,
            PhoneNumber = addEmployeeDto.PhoneNumber,
            ManagerId = addEmployeeDto.ManagerId,

        };

        var createdEmployee = _employeeRepository.Create(employee);
        if (createdEmployee is null)
        {
            return null; // employee not created
        }

        Account account = new Account
        {
            Guid = employee.Guid,
            Email = addEmployeeDto.Email,
            Password = Hashing.HashPassword(addEmployeeDto.Password),
        };
        var createdAccount = _accountRepository.Create(account);
        if (createdAccount is null)
        {
            return null;
        }

        var role = _roleRepository.GetByName("User");

        if (role is null)
        {
            return null;
        }

        var toDto = new AddEmployeeDto
        {
            FirstName = createdEmployee.FirstName,
            LastName = createdEmployee.LastName,
            Gender = createdEmployee.Gender,
            PhoneNumber = createdEmployee.PhoneNumber,
            ManagerId = createdEmployee.ManagerId,
            Email = createdAccount.Email,
            Password = createdAccount.Password
        };

        return toDto; // employee created
    }


}