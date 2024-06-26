﻿using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    public class Booking
    {
        [Display(Name = "Booking ID")]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? EventId { get; set; }
        public User? User { get; set; }
        public Event? Event { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        public Boolean? IsBooked { get; set; }

        public Booking()
        {
            this.IsBooked = false;
        }
        
        
    }
}
