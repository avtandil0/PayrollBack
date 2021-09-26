﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class PaymentDaysType
    {
        public PaymentDaysType()
        {
            EmployeeComponents = new HashSet<EmployeeComponent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<EmployeeComponent> EmployeeComponents { get; set; }
    }
}
