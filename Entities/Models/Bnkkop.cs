using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Bnkkop
    {
        public int Id { get; set; }
        public string BankRek { get; set; }
        public string CodeDc { get; set; }
        public string Crdnr { get; set; }
        public string Debnr { get; set; }
        public DateTime Syscreated { get; set; }
        public int Syscreator { get; set; }
        public DateTime Sysmodified { get; set; }
        public int Sysmodifier { get; set; }
        public Guid Sysguid { get; set; }
        public byte[] Timestamp { get; set; }
        public Guid? CntId { get; set; }
        public short? Division { get; set; }
    }
}
