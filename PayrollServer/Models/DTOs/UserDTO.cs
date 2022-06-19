using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class UserDTO
    {
        public Guid userId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }



        public bool IsActive { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public Guid Role { get; set; }

    }
}
