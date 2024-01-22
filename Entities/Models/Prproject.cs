using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Prproject
    {
        public int Id { get; set; }
        public string ProjectNr { get; set; }
        public string ParentProject { get; set; }
        public int? Idjobgroup { get; set; }
        public int? IdjobActivity { get; set; }
        public string Description { get; set; }
        public DateTime? Syscreated { get; set; }
        public int? Syscreator { get; set; }
        public DateTime? Sysmodified { get; set; }
        public int? Sysmodifier { get; set; }
        public Guid? Idcustomer { get; set; }
        public Guid? Idreseller { get; set; }
        public string CostCenter { get; set; }
        public int? Assortment { get; set; }
        public string Release { get; set; }
        public int? Responsible { get; set; }
        public string Division { get; set; }
        public string Memo { get; set; }
        public byte[] Image { get; set; }
        public string ImageFilename { get; set; }
        public string ItemCode { get; set; }
        public Guid? IditemNumbers { get; set; }
        public double? EstRev { get; set; }
        public double? EstCost { get; set; }
        public string EstRevCcy { get; set; }
        public string EstCostCcy { get; set; }
        public DateTime? InitialStartDate { get; set; }
        public DateTime? InitialEndDate { get; set; }
        public Guid? Dlconfirmation { get; set; }
        public Guid? Dlproposal { get; set; }
        public Guid? Dlevaluation { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int? Security { get; set; }
        public byte? VisibleMember { get; set; }
        public DateTime? DateField1 { get; set; }
        public DateTime? DateField2 { get; set; }
        public DateTime? DateField3 { get; set; }
        public DateTime? DateField4 { get; set; }
        public DateTime? DateField5 { get; set; }
        public double? NumberField1 { get; set; }
        public double? NumberField2 { get; set; }
        public double? NumberField3 { get; set; }
        public double? NumberField4 { get; set; }
        public double? NumberField5 { get; set; }
        public byte? YesNoField1 { get; set; }
        public byte? YesNoField2 { get; set; }
        public byte? YesNoField3 { get; set; }
        public byte? YesNoField4 { get; set; }
        public byte? YesNoField5 { get; set; }
        public string TextField1 { get; set; }
        public string TextField2 { get; set; }
        public string TextField3 { get; set; }
        public string TextField4 { get; set; }
        public string TextField5 { get; set; }
        public string HourlyRateBasis { get; set; }
        public double HourlyRate { get; set; }
        public double? Complete { get; set; }
        public string MaterialLine { get; set; }
        public string Costunit { get; set; }
        public string WipAccount { get; set; }
        public string Labour { get; set; }
        public string Machine { get; set; }
        public string Material { get; set; }
        public byte BackFlushing { get; set; }
        public string WipRevenue { get; set; }
        public string WipMethod { get; set; }
        public string HourlyRateBasisExt { get; set; }
        public string Magcode { get; set; }
        public string SerialNumber { get; set; }
        public byte[] Timestamp { get; set; }
        public byte? HourMember { get; set; }
        public byte? LinkHour { get; set; }
        public short? DivisionX { get; set; }
        public Guid Sysguid { get; set; }
        public string Compression { get; set; }
        public string DrawingNumber { get; set; }
        public byte? Poyes { get; set; }
        public byte SubType { get; set; }
        public double? Vatamount { get; set; }
        public double? VatpoOriginalCurrency { get; set; }
        public DateTime? CspickitlatestTryInService { get; set; }
    }
}
