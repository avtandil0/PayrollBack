using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.HelperModels
{
    public class UserRuleModel
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
