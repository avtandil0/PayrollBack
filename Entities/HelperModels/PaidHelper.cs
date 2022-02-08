using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.HelperModels
{
    public class PaidHelper
    {
        public DateTime PaidDate { get; set; }
        public DateTime PaidPeriod { get; set; }
        public Guid ComponentID { get; set; }
        public List<ImportPerson> Persons { get; set; }

    }
}
