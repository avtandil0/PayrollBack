using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.HelperModels
{
    public class ImportPerson
    {
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string Destination { get; set; }
        public string FullName { get; set; }
        public string Amount { get; set; }
    }
}
