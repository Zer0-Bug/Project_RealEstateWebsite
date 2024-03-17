using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Models
{
    public class ProfilGuncelleme
    {
        public string id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Please enter an valid email address...")]
        public string Email { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string Username { get; set; }
    }
}