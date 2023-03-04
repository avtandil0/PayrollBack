using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public partial class Rate
    {
        public Guid Id { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Currency { get; set; }
        public DateTime? Date { get; set; }
    }
}
