using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.ViewModels
{
    public class ParticipantViewModel
    {
        public int Id { get; set; }
        public int TeamId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name must be 50 characters or less.")]
        [RegularExpression("([A-Za-z \'-])*", ErrorMessage = "First Name is not valid.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name must be 50 characters or less.")]
        [RegularExpression("([A-Za-z \'-])*", ErrorMessage = "Last Name is not valid.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address must be 200 characters or less.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City must be 50 characters or less.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip is required.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Zip must be between 5 and 10 characters long.")]
        [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$)", ErrorMessage = "The zipcode must match a valid format.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [StringLength(20, ErrorMessage = "Phone must be 20 characters or less.")]
        [Phone]
        public string Phone { get; set; }

        [StringLength(200, ErrorMessage = "Email must be 200 characters or less.")]
        [EmailAddress(ErrorMessage = "Email is not a valid e-mail address.")]
        [Required(ErrorMessage = "EMail is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birthdate is required.")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "You must select a value for feet for your height.")]
        //public HeightFeetViewModel HeightFeet { get; set; }
        public int HeightFeet { get; set; }

        [Required(ErrorMessage = "You must select a value for inches for your height.")]
        //public HeightInchesViewModel HeightInches { get; set; }
        public int HeightInches { get; set; }

        [Required(ErrorMessage = "You must select a gender.")]
        //public GenderStringViewModel Gender { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "You must select a value for grade.")]
        //public GradeViewModel NextGrade { get; set; }
        public int NextGrade { get; set; }

        [Required(ErrorMessage = "You must select a value for playing experience.")]
        //public PlayingExperienceViewModel PlayingExperience { get; set; }
        public int PlayingExperience { get; set; }

        [Required(ErrorMessage = "You must select a value for playing frequency.")]
        //public PlayingFrequencyViewModel PlayingFrequency { get; set; }
        public int PlayingFrequency { get; set; }

        [Required(ErrorMessage = "You must select a T-Shirt size.")]
        //public ShirtSizeViewModel ShirtSize { get; set; }
        public string ShirtSize { get; set; }

        [Required(ErrorMessage = "You must select if you played last year.")]
        public bool PlayedLastYear { get; set; }
    }
}
