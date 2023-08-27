using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NatureNexus.Models;

namespace NatureNexus.Data
{
    public class AppContext : IdentityDbContext<AppUser>
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}
