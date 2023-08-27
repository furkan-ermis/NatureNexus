using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureNexus.Models;
using System.Security.Claims;

namespace NatureNexus.Controllers
{
    public class RegisterController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public RegisterController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            if ((Request.Cookies.ContainsKey("email") & Request.Cookies.ContainsKey("passWord")))
            {
                // --------- Beni Hatırla aktif ise --------------
                ViewBag.email = Request.Cookies["email"];
                ViewBag.password = Request.Cookies["passWord"];
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(UserLogInViewModel userLogInViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = _userManager.Users.FirstOrDefault(x => x.Email == userLogInViewModel.Email);

                var result = await _signInManager.PasswordSignInAsync(appUser.UserName, userLogInViewModel.Password, false, true);
                if (result.Succeeded)
                {
                    // --------- Beni Hatırla aktif olduğunda cookie oluştur --------------

                    if (userLogInViewModel.RememberMe)
                    {
                        Response.Cookies.Append("email", userLogInViewModel.Email);
                        Response.Cookies.Append("passWord", userLogInViewModel.Password);
                    }
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    return RedirectToAction("SignUp", "Register");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel userSignUpViewModel)
        {
            IdentityResult identityResult;
            if (ModelState.IsValid)
            {
                AppUser isExist = _userManager.Users.FirstOrDefault(x => x.Email == userSignUpViewModel.Email);
                if (isExist != null)
                {
                    ModelState.AddModelError("Email", "Bu adres kayıtlı");
                    return View();
                }
                else
                {
                    //----------- Yeni Üye Oluşturma ------------
                    AppUser appUser = new AppUser()
                    {
                        UserName = userSignUpViewModel.Username,
                        Email = userSignUpViewModel.Email,
                        Name = userSignUpViewModel.Name,
                        Surname = userSignUpViewModel.Surname,
                    };
                    identityResult = _userManager.CreateAsync(appUser, userSignUpViewModel.Password).Result;
                    if (identityResult.Succeeded)
                    {
                        return RedirectToAction("LogIn", "Register");
                    }
                    foreach (IdentityError item in identityResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(userSignUpViewModel);
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {

            return View();
        }


        public async Task<IActionResult> ForgetPassword(string email)
        {
            AppUser appUser;

            string resetToken;

            if (ModelState.IsValid)
            {
                appUser = _userManager.Users.FirstOrDefault(x => x.Email == email);
                if (appUser != null)
                {
                    resetToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    new SendMail(email, "10webapp10@gmail.com", "mnvvwljcchhtsaig", "Admin-NatureNexus",
                        email, "Şifre Yenileme", "Şifre Yenileme için Kod : " + resetToken);

                    ViewData["eMail"] = email;
                    return View("ResetPassword");
                }
                ModelState.AddModelError("eMail", "Kullanıcı Bulunamadı");
            }
            ModelState.AddModelError("eMail", "Geçersiz Bir Mail Adresi Girildi");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            AppUser applicationUser;
            IdentityResult identityResult;

            if (ModelState.IsValid)
            {
                applicationUser = _userManager.Users.FirstOrDefault(x => x.Email == resetPasswordViewModel.EMail);
                applicationUser.UserName = applicationUser.UserName.Trim();

                identityResult = await _userManager.ResetPasswordAsync(applicationUser, resetPasswordViewModel.Code, resetPasswordViewModel.PassWord);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("LogIn", "Register");
                }



                ModelState.AddModelError("Code", "Doğru Kod Girdiğinizden Emin olun");

            }
            ModelState.AddModelError("PassWord", "Geçersiz Bir Parola Girildi");

            return View();
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Account(string? id)
        {

            AppUser? user;

            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            user = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
