using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Hrjbtl
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Descr50 { get; set; }
        public int JobLevel { get; set; }
        public string JobGrp { get; set; }
        public string VertGrp { get; set; }
        public int? Notes { get; set; }
        public string DelDocPath { get; set; }
        public string DelDocName { get; set; }
        public string BeroepCd { get; set; }
        public string CodeFnc { get; set; }
        public string Schaalsrt { get; set; }
        public string Schaalcode { get; set; }
        public double Kmrateint { get; set; }
        public byte Productiv { get; set; }
        public double Prodpct { get; set; }
        public string Xdefratelv { get; set; }
        public double Inttar { get; set; }
        public double Exttar { get; set; }
        public byte[] Timestamp { get; set; }
        public Guid? DocId { get; set; }
        public int? RoleId { get; set; }
        public int? RoleLevel { get; set; }
        public int ScaleMin { get; set; }
        public int ScaleMax { get; set; }
        public string WkcpRisk { get; set; }
        public short? Division { get; set; }
        public DateTime Syscreated { get; set; }
        public int Syscreator { get; set; }
        public DateTime Sysmodified { get; set; }
        public int Sysmodifier { get; set; }
        public Guid Sysguid { get; set; }
    }
}
