using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.WebUI.Data;

public class AppUserDbContext(DbContextOptions<AppUserDbContext> options) : IdentityDbContext<AppUser>(options) {
}