using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

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
        public IActionResult Index()
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
                return View("Index");
            }

            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            
            var passwordHasher = new PasswordHasher<UserModel>();
            user.Password = passwordHasher.HashPassword(user, newPassword);

            db.SaveChanges();

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
