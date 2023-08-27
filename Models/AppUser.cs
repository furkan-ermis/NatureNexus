using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NatureNexus.Models
{
    public class AppUser : IdentityUser
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

    }
}
