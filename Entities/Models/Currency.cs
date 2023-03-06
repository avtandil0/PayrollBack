using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Rates = new HashSet<Rate>();
        }

        public int Id { get; set; }
        public string Currency1 { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
