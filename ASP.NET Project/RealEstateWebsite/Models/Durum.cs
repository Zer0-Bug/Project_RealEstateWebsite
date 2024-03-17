using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Models
{
    public class Durum
    {
        public int DurumId { get; set; }
        public string DurumAd { get; set; }
        public int Confirmed { get; set; }
        public List<Tip> Tips { get; set; }
    }
}