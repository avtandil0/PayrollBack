using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public partial class Rate
    {
        public Guid Id { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? ExchangeRate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
