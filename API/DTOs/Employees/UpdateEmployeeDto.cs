﻿using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employees
{
    public class UpdateEmployeeDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public int NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "0 = Female, 1 = Male ")]
        public GenderEnum Gender { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public int EligibleLeave { get; set; }
        public Guid? ManagerGuid { get; set; }
    }
}
