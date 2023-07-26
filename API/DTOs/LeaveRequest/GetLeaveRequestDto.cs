﻿using API.Utilities.Enums;

namespace API.DTOs.LeaveRequest;

public class GetLeaveRequestDto
{
    public Guid Guid { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime SubmitDate { get; set; }
    public string Remarks { get; set; }
    public string? Attachment { get; set; }
    public Guid LeaveTypesGuid { get; set; }
    public Guid EmployeesGuid { get; set; }
}
