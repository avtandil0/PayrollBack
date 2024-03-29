﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class CoefficientGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid? PensionCoefficientId { get; set; }
        public Guid? StandartCoefficientId { get; set; }

        public virtual Coefficient PensionCoefficient { get; set; }
        public virtual Coefficient StandartCoefficient { get; set; }
    }
}
