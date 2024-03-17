using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Models
{
    public class Mahalle
    {
        public int MahalleId { get; set; }
        public string MahalleAd { get; set; }
        public int SemtId { get; set; }
        /*Bir mahallenin bir semti vardır ve bir semtin birden fazla mahallesi olabilir*/
        public virtual Semt Semt { get; set; }
        /*Program anında mahallenin semt bilgilerine ulaşabilmek için kullanılacak
         */
        //public bool degısıklık { get; set; }
    }
}