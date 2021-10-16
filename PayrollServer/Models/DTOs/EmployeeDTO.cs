using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public int ResId { get; set; }
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
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public List<EmployeeComponentDTO> EmployeeComponents { get; set; }
    }
}
