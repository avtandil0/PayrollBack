using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeComponents = new HashSet<EmployeeComponent>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string PersonalNumber { get; set; }
        public string Address { get; set; }
        public string BankAccountNumber { get; set; }
        public int SchemeTypeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Department Department { get; set; }
        public virtual SchemeType SchemeType { get; set; }
        public virtual ICollection<EmployeeComponent> EmployeeComponents { get; set; }
    }
}
