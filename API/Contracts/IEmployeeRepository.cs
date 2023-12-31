﻿using API.Models;

namespace API.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    bool IsDuplicateValue(string value);
    IEnumerable<Employee> GetEmployeesByManagerGuid(Guid managerGuid);
}
