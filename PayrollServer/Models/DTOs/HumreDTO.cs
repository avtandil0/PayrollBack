using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class HumreDTO
    {
        public int Id { get; set; }
        public int ResId { get; set; }
        public string Fullname { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Initialen { get; set; }
        public string Mv1 { get; set; }
        public string Adres1 { get; set; }
        public string Adres2 { get; set; }
        public string Woonpl { get; set; }
        public string Postcode { get; set; }
        public string LandIso { get; set; }
        public string IsoTaalcd { get; set; }
    }
}
