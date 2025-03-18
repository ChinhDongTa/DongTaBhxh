using Microsoft.AspNetCore.Identity;

namespace DongTaBhxh.WebUI.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser {
    public int EmployeeId { get; set; }
}