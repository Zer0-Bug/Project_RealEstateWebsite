using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Models
{
    public class Tip
    {
        public int TipId { get; set; }
        public string TipAd { get; set; }
        public int DurumId { get; set; }
        /*Bir tipin bir surumu vardır.Yani ev aynı anda hem satılık hem kiralık olamaz*/
        public virtual Durum Durum { get; set; }
    }
}