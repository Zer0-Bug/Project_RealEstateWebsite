using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWebsite.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address..")]
        public string Email { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords are different...")]
        [DisplayName("Password Again")]
        public string RePassword { get; set; }
    }
}