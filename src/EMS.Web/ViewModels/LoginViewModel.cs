using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.ViewModels
{
    public class LoginViewModel
    {
        [StringLength(200, ErrorMessage = "Email Address must be 200 characters or less.")]
        [EmailAddress(ErrorMessage = "Email is not a valid e-mail address.")]
        [Required(ErrorMessage = "EMail Address is required.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters and no longer than 20 characters.")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
