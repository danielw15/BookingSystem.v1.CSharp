using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingSystem.Models
{
    public class UserRoleViewModel
    {
        public List<User>? Users { get; set; }
        public SelectList? Roles { get; set; }
        public string? UserRole {  get; set; }

        public string? SearchString { get; set; }
    }
}
