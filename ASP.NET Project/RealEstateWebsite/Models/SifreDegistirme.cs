using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Models
{
    public class SifreDegistirme
    {
        [Required]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [DisplayName("New Password")]
        /*Şifre kısıtlamaları*/
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Your password must be at least 5 characters...")]
        public string NewPassword { get; set; }
        [Required]
        [DisplayName("Password Again")]
        [Compare("NewPassword", ErrorMessage = "Different Passwords..")]
        public string ConNewPassword { get; set; }
    }
}
