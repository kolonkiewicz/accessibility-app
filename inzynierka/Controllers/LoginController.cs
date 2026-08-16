using inzynierka.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace inzynierka.Controllers
{
    public class LoginController : Controller
    {
        private readonly InzynierkaContext _context;

        public LoginController(InzynierkaContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitLogin(UserModel user)
        {

            var existinguser = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (existinguser == null)
            {
                TempData["DangerMessage"] = "Wprowadź poprawne dane!";
                ModelState.AddModelError("Username", "Podaj poprawny login");
                return View("Index");   
            }

            if (!existinguser.EmailConfirmed)
            {
                TempData["DangerMessage"] = "Musisz potwierdzić adres e-mail, zanim się zalogujesz.";
                ModelState.AddModelError("Username", "Konto nieaktywne – sprawdź skrzynkę pocztową.");
                return View("Index");   
            }

            var passwordHasher = new PasswordHasher<UserModel>();
            var veryficationResult = passwordHasher.VerifyHashedPassword(
                existinguser,
                existinguser.Password,
                user.Password
            );

            if (veryficationResult == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Password", "Podaj poprawne hasło");
                TempData["DangerMessage"] = "Wprowadź poprawne dane!";
                return View("Index");   
            }

            HttpContext.Session.SetString("SessionRole", existinguser.Role);
            HttpContext.Session.SetString("SessionIdUser", existinguser.UserId.ToString());

            TempData["SuccessMessage"] = "Logowanie zakończone sukcesem!";
            return RedirectToAction("Index", "Account");
        }
    }
}
