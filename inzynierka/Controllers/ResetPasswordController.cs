using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace inzynierka.Controllers
{
    public class ResetPasswordController : Controller
    {

        private readonly EmailService _emailService;

        public ResetPasswordController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string Email)
        {
            using var db = new InzynierkaContext();

            var user = db.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
            {
                TempData["DangerMessage"] = "Nie znaleziono użytkownika o podanym adresie email.";
                return View();
            }

            // 🔥 generujemy nowe losowe hasło
            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);


            user.Password = newPassword;
            db.SaveChanges();

            // 🔥 wyślij maila (używasz tego samego co w potwierdzaniu)
            _emailService.SendEmailAsync(
                Email,
                "Przypomnienie hasła",
                $"Twoje nowe hasło to: <b>{newPassword}</b><br><br>Zalecamy zmianę po zalogowaniu."
            );

            TempData["SuccessMessage"] = "Nowe hasło zostało wysłane na email.";

            return RedirectToAction("GoToLogin", "Home");
        }
    }
}
