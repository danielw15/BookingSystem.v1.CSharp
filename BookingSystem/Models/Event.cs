using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    public class Event
    {
        [Display(Name = "Event ID")]
        public int Id { get; set; }
        [RegularExpression(@"^[0-9]+$")]
        [Required]
        [Display(Name = "User ID")]
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        public string? UserFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? UserLastName { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string? Title { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string? Description { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set;}
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set;}
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set;}
        [Display(Name = "Creator Name")]
        public string getUserFullName 
        {
            get
            {
                return UserFirstName + " " + UserLastName;
            }
        }

        
    }
}
