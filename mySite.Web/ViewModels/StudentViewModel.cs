using System.ComponentModel.DataAnnotations;
namespace mySite.Web.ViewModels
{
    public class StudentViewModel
    {
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Display(Name = "Surname:")]
        public string Surname { get; set; }

        [Display(Name = "University:")]
        public string University { get; set; }
    }
}