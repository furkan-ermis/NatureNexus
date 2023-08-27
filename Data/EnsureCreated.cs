using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NatureNexus.Data;
using Microsoft.AspNetCore.Identity;
using NatureNexus.Models;

namespace OzSapkaTShirt.Data
{
    // Sistem Başlatılırken Database'de Oluşması istenen veriler için
    public class EnsureCreated
    {
        private readonly UserManager<AppUser> _userManager;
        public EnsureCreated(NatureNexus.Data.AppContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            createAdmin();
        }
        // Admin Kullanıcısı Oluşturma Metodu
        public void createAdmin()

        {
            IdentityResult? identityResult;

            AppUser user = new AppUser()
            {
                Name = "Admin",
                Surname = "Adminstrator",
                UserName = "Admin10",
                Email = "10webapp10@gmail.com"
            };
            AppUser isExist = _userManager.FindByNameAsync(user.UserName).Result;
            if (isExist == null)
            {
                identityResult = _userManager.CreateAsync(user, "123Abc123").Result;
            }

        }
    }
}
