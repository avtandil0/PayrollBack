using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.HelperModels
{
    public class UserModel
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
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        public List<Guid> UserClaimTypes { get; set; }

        public UserRuleModel Roles { get; set; }
    }
}
