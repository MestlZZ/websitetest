using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyPlan.Web.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("([\\S]{7,})", ErrorMessage = "You can't input space symbols, and length must be 7 or more")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}