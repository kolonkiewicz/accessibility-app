using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace inzynierka.Controllers
{
    public class RegisterController : Controller
    {
        private readonly EmailService _emailService;

        public RegisterController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitRegister(UserModel user)
        {
            using InzynierkaContext inzynierkaContext = new InzynierkaContext();

            var existinglogin = inzynierkaContext.Users.FirstOrDefault(u => u.Username == user.Username);
            var existingemail = inzynierkaContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingemail == null)
            {
                if (existinglogin == null)
                {
                    user.Role = "user";

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            // 🔥 GENEROWANIE TOKENU 🔥
                            user.VerificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                            user.VerificationTokenExpires = DateTime.UtcNow.AddHours(24);
                            user.EmailConfirmed = false;

                            inzynierkaContext.Users.Add(user);
                            inzynierkaContext.SaveChanges();

                            // 🔥 WYŚLIJ MAILA 🔥
                            string link = Url.Action(
                                "VerifyEmail",
                                "Register",
                                new { token = user.VerificationToken },
                                Request.Scheme);

                            _emailService.SendEmailAsync(
                                user.Email,
                                "Potwierdzenie rejestracji",
                                $"Kliknij aby potwierdzić konto: <a href='{link}'>POTWIERDŹ</a>"
                            );

                            TempData["SuccessMessage"] = "Rejestracja zakończona — sprawdź email aby potwierdzić konto!";
                            return RedirectToAction("GoToLogin", "Home");
                        }
                        catch (Exception)
                        {
                            TempData["DangerMessage"] = "Coś poszło nie tak";
                            return View("Index", user);
                        }
                    }
                    else
                    {
                        TempData["DangerMessage"] = "Uzupełnij dane";
                        return View("Index", user);
                    }
                }
                else
                {
                    TempData["DangerMessage"] = "Wprowadź poprawne dane!";
                    ModelState.AddModelError("Username", "Podany username już istnieje");
                    return View("Index", user);
                }
            }
            else
            {
                TempData["DangerMessage"] = "Wprowadź poprawne dane!";
                ModelState.AddModelError("Email", "Email już istnieje");
                return View("Index", user);
            }
        }

        // 🔥 AKCJA POTWIERDZAJĄCA EMAIL 🔥
        public IActionResult VerifyEmail(string token)
        {
            using InzynierkaContext inzynierkaContext = new InzynierkaContext();

            if (token == null)
                return BadRequest("Niepoprawny token");

            var user = inzynierkaContext.Users.FirstOrDefault(u => u.VerificationToken == token);

            if (user == null)
                return BadRequest("Nieprawidłowy token");

            if (user.VerificationTokenExpires < DateTime.UtcNow)
                return BadRequest("Token wygasł");

            user.EmailConfirmed = true;
            user.VerificationToken = null;
            user.VerificationTokenExpires = null;

            inzynierkaContext.SaveChanges();

            TempData["SuccessMessage"] = "Email potwierdzony! Możesz się zalogować.";
            return RedirectToAction("GoToLogin", "Home");
        }
    }
}
