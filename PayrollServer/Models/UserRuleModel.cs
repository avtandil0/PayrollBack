using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models
{
    public class UserRuleMode
    {
        public bool ViewPersonsList { get; set; }

        public bool AddEditUser { get; set; }

        public bool AddEditPersonMonitoring { get; set; }

        public bool AddEditPersonInspection { get; set; }

        public bool AddEditViewConfiguration { get; set; }

        public bool AddEditViewUsers { get; set; }



        public bool Admin { get; set; }
        public bool Operator { get; set; }
        public bool Analyst { get; set; }
    }
}
