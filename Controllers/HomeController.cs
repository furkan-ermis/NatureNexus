using Microsoft.AspNetCore.Mvc;
using NatureNexus.Models;
using Org.BouncyCastle.Crypto.Macs;
using System.Diagnostics;

namespace NatureNexus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //-----------Footer Tarih Fonksiyonu-------
        public ActionResult GetCurrentDate()
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return Content(currentDate);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        // --------- İletişim İçin Mail Gönderimi --------------
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            string feedBack = "Geri Bildirim İçin Teşekkürler";
            string feedBackMessage = "Ekibimizden Biri En Yakın Zamanda Sizinle İletişime Geçecektir Teşekkür Ederiz.";

            new SendMail(contactViewModel.Mail, "10webapp10@gmail.com", "mnvvwljcchhtsaig", "Admin-NatureNexus", contactViewModel.Name, feedBack, feedBackMessage);
            new SendMail("10webapp10@gmail.com", "10webapp10@gmail.com", "mnvvwljcchhtsaig", "Admin-NatureNexus", contactViewModel.Name, "Bir Bildiriminiz Var", contactViewModel.Message);
            return View("Index");
        }
        [HttpPost]
        public IActionResult NeedHelpMail(string sendmail)
        {
            new SendMail(sendmail, "10webapp10@gmail.com", "mnvvwljcchhtsaig", "Admin-NatureNexus", "User", "Bir Bildiriminiz Var", "Kullanıcı Yardım İstedi");
            return View("Index");
        }
        public IActionResult About()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}