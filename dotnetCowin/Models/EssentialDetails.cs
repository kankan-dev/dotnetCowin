using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetCowin.Models
{
    public partial class EssentialDetails
    {
        public string name { get; set; }
        public string address { get; set; }
        public List<VaccineFeeDetails> VaccineFees { get; set; }
        
    }
    public partial class VaccineFeeDetails
    {
        public string VaccineName { get; set; }
        public long Fees { get; set; }



    }

}
