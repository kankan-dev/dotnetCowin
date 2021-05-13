using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetCowin.Models
{
    public class TweetDetails
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public long Pincode { get; set; }
        
        public string Fee { get; set; }

        public long AvailableCapacity { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }
}
