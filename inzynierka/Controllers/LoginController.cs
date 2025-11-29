using inzynierka.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace inzynierka.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitLogin(UserModel user)
        {
            using InzynierkaContext cloudContext = new InzynierkaContext();

            var existinguser = cloudContext.Users.FirstOrDefault(u => u.Username == user.Username);

            if (existinguser == null)
            {
                TempData["DangerMessage"] = "Wprowadź poprawne dane!";
                ModelState.AddModelError("Username", "Podaj poprawny login");
                return View("Login");
            }

            if (!existinguser.EmailConfirmed == true)
            {
                TempData["DangerMessage"] = "Musisz potwierdzić adres e-mail, zanim się zalogujesz.";
                ModelState.AddModelError("Username", "Konto nieaktywne – sprawdź skrzynkę pocztową.");
                return View("Login");
            }

            if (existinguser.Password != user.Password)
            {
                ModelState.AddModelError("Password", "Podaj poprawne hasło");
                TempData["DangerMessage"] = "Wprowadź poprawne dane!";
                return View("Login");
            }

            HttpContext.Session.SetString("SessionRole", existinguser.Role);
            HttpContext.Session.SetString("SessionIdUser", existinguser.UserId.ToString());

            TempData["SuccessMessage"] = "Logowanie zakończone sukcesem!";

            return RedirectToAction("GoToAccount", "Home");
        }


    }
}
