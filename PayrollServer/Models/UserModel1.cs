using Entities.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models
{
    public class UserModel1
    {
        public string UserId { get; set; }

        public Guid? AspNetUserTypeId { get; set; }

        public Guid? UserOrganizationId { get; set; }
        public Guid? MedicalOrganizationId { get; set; }

        public string UserName { get; set; }

        public string PersonalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

       

        public string Position { get; set; }


        public bool IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        public List<Guid> UserClaimTypes { get; set; }

        public UserRuleModel Roles { get; set; }
    }
}