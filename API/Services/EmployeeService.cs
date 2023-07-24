﻿using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
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
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly MyDbContext _myDbContext;

    public EmployeeService(IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IRoleRepository roleRepository, MyDbContext myDbContext, IAccountRoleRepository accountRoleRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
        _myDbContext = myDbContext;
        _accountRoleRepository = accountRoleRepository;
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
                                               EligibleLeave = employee.EligibleLeave,
                                               ManagerGuid = employee.ManagerGuid
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
            EligibleLeave = employee.EligibleLeave,
            ManagerGuid = employee.ManagerGuid
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
            EligibleLeave = newEmployeeDto.EligibleLeave,
            ManagerGuid = newEmployeeDto.ManagerGuid
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
            EligibleLeave = employee.EligibleLeave,
            ManagerGuid = employee.ManagerGuid
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
            EligibleLeave = updateEmployeeDto.EligibleLeave,
            ManagerGuid = updateEmployeeDto.ManagerGuid
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

    public IEnumerable<GetDataEmployeeDto>? GetDataEmployee()
    {

        var master = (from employee in _employeeRepository.GetAll()
                      join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                      select new GetDataEmployeeDto
                      {
                          Guid = employee.Guid,
                          FullName = employee.FirstName + " " + employee.LastName,
                          NIK = employee.NIK,
                          Email = account.Email,
                          PhoneNumber = employee.PhoneNumber,
                          Gender = employee.Gender,
                          EligibleLeave = employee.EligibleLeave,
                          ManagerGuid = employee.ManagerGuid
                      }).ToList();

        if (!master.Any())
        {
            return null;
        }

        return master;
    }

    public AddEmployeeDto? AddEmployee(AddEmployeeDto addEmployeeDto)
    {
       var transaction = _myDbContext.Database.BeginTransaction();

        try
        {
            /*if (addEmployeeDto.Password != addEmployeeDto.ConfirmPassword)
            {
                return null;
            }*/

            Employee employee = new Employee
            {
                Guid = new Guid(),
                NIK = GenerateNik(),
                FirstName = addEmployeeDto.FirstName,
                LastName = addEmployeeDto.LastName,
                Gender = addEmployeeDto.Gender,
                PhoneNumber = addEmployeeDto.PhoneNumber,
                EligibleLeave = addEmployeeDto.EligibleLeave,
                ManagerGuid = addEmployeeDto.ManagerGuid ?? null,

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
                var dto = new
                {
                    Name = "User"
                };
                var createdRole = new Role
                {
                    Guid = Guid.NewGuid(),
                    Name = dto.Name,
                };
                var created = _roleRepository.Create(createdRole);
                if (created == null)
                {
                    return null;
                }

                var accountRole = new AccountRole
                {
                    Guid = Guid.NewGuid(),
                    AccountGuid = account.Guid,
                    RoleGuid = createdRole.Guid
                };

                var createdAccountRole = _accountRoleRepository.Create(accountRole);
                if (createdAccountRole == null)
                {
                    return null;
                }
            }
            else
            {
                var accountRole = new AccountRole
                {
                    Guid = Guid.NewGuid(),
                    AccountGuid = account.Guid,
                    RoleGuid = role.Guid
                };

                var createdAccountRole = _accountRoleRepository.Create(accountRole);
                if (createdAccountRole == null)
                {
                    return null;
                }

            }
            var toDto = new AddEmployeeDto
            {
                FirstName = createdEmployee.FirstName,
                LastName = createdEmployee.LastName,
                Gender = createdEmployee.Gender,
                PhoneNumber = createdEmployee.PhoneNumber,
                EligibleLeave = createdEmployee.EligibleLeave,
                ManagerGuid = createdEmployee.ManagerGuid ?? null,
                Email = createdAccount.Email,
                Password = createdAccount.Password
            };
            transaction.Commit();
            return toDto; // employee created
        }
        catch
        {
            transaction.Rollback();
            return null;
        }
      
    }


}