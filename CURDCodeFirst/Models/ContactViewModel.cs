using System.ComponentModel.DataAnnotations;

namespace CURDCodeFirst.Models
{
    public class ContactViewModel
    {
        public long Id { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; }
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
    }
}