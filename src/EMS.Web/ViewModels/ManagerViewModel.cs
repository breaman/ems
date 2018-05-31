using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.ViewModels
{
    public class ManagerViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "{0} can't be longer than {1} characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "{0} can't be longer than {1} characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address must be 200 characters or less.")]
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }

        [StringLength(200, ErrorMessage = "Address2 must be 200 characters or less.")]
        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }

        [Display(Name = "Country")]
        [Required]
        [StringLength(10, ErrorMessage = "Country must be 10 characters or less.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City must be 50 characters or less.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(5, ErrorMessage = "State/Province must be 5 characters or less.")]
        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }

        [Required(ErrorMessage = "Zip is required.")]
        [StringLength(10, ErrorMessage = "Zip must be 10 characters or less.")]
        [Display(Name = "Zip/Postal Code")]
        public string Zip { get; set; }

        [Phone]
        [Required(ErrorMessage = "The Primary Phone field is required.")]
        [Display(Name = "Primary Phone")]
        public string PhoneNumber { get; set; }

        [Phone]
        [Display(Name = "Secondary Phone (optional)")]
        public string SecondaryPhoneNumber { get; set; }

        [Display(Name = "Other Country")]
        public string OtherCountry { get; set; }

        [Display(Name = "Other State/Province")]
        public string OtherStateProvince { get; set; }

        [StringLength(200, ErrorMessage = "Email Address must be 200 characters or less.")]
        [EmailAddress(ErrorMessage = "Email is not a valid e-mail address.")]
        [Required(ErrorMessage = "EMail Address is required.")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
