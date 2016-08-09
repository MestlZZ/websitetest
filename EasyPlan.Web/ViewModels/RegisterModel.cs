using System.ComponentModel.DataAnnotations;

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
        [RegularExpression("([\\S]{7,})", ErrorMessage = "You must input at least 7 no space symbols")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}