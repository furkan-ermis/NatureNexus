using Microsoft.AspNetCore.Mvc;

namespace NatureNexus.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

    }
}
