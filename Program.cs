using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NatureNexus.Models;
using NatureNexus.Data;
using System.Web.Optimization;
using DartSassHost;
using OzSapkaTShirt.Data;

var builder = WebApplication.CreateBuilder(args);

UserManager<AppUser> userManager;
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NatureNexus.Data.AppContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultContext") ?? throw new InvalidOperationException("Connection string 'DBContext' not found.")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; options.Password.RequireNonAlphanumeric = false; }).AddDefaultTokenProviders()
     .AddEntityFrameworkStores<NatureNexus.Data.AppContext>();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}



NatureNexus.Data.AppContext context = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetService<NatureNexus.Data.AppContext>();
context.Database.EnsureCreated();
userManager = app.Services.CreateScope().ServiceProvider.GetService<UserManager<AppUser>>();
EnsureCreated ensureCreated = new EnsureCreated(context, userManager);



app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();