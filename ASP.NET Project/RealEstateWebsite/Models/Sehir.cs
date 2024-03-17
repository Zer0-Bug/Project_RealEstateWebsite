using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Models
{
    public class Sehir
    {
        public int SehirId { get; set; }
        public string SehirAd { get; set; }
        public List<Semt> Semts { get; set; }
        /*Burda ise şehrin bütün semtlerini getirmek için list kullandık*/
        
    }
}