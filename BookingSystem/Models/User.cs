using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    public class User
    {
        [Display(Name = "User ID")]
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Display(Name = "First Name")]
        [Required]
        public string? FirstName { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        [Required]
        public string? LastName { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Email { get; set; }
        [StringLength(60, MinimumLength = 8)]
        [Required]
        public string? Password { get; set; }
        [RegularExpression(@"^[0-9]+$")]
        [Required]
        public string? PhoneNumber { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Role { get; set; }
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}