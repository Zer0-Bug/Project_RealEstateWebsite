using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Models
{
    public class Semt
    {
        public int SemtId { get; set; }
        public string SemtAd { get; set; }
        public int SehirId { get; set; }
        /*Bir semtin bir şehri vardır ve bir şehrin birden fazla semti olabilir.*/
        public virtual Sehir Sehir { get; set; }
        /*Program anında semtin(ilçenin şehir bilgilerine gidebilmek için semt ve şehir bu şekilde bağlanmalı)*/
        public List<Mahalle> Mahalles { get; set; }
    }
}