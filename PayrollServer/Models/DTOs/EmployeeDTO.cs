using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PayrollServer.Models.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public int? ResId { get; set; }
        public byte[] Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string PersonalNumber { get; set; }
        public string Address { get; set; }
        public string BankAccountNumber { get; set; }
        public int SchemeTypeId { get; set; }
        public string SchemeTypeName { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Position { get; set; }
        public string LandIso { get; set; }
        public int? EmployeeTypeId { get; set; }
        public int? EmployeeGraceTypeId { get; set; }
        public decimal? GraceAmount { get; set; }
        public decimal? RemainingGraceAmount { get; set; }
        public decimal? TotalBalance { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
       
        public virtual ICollection<CalculationDTO> Calculations { get; set; }
        public virtual ICollection<EmployeeComponentDTO> EmployeeComponents { get; set; }
    }
    
    public class Avatar
    {
        public Guid userId { get; set; }
        public IFormFile File { get; set; }
    }
}
