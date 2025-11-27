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
        public ActionResult SubmitLogin(UserModel user) {

            using CloudContext cloudContext = new CloudContext();

            var existinguser = cloudContext.Users.FirstOrDefault(u => u.Username == user.Username );

            if (existinguser != null)
            {
                if (existinguser.Password == user.Password)
                {
                    HttpContext.Session.SetString("SessionRole", existinguser.Role);
                    HttpContext.Session.SetString("SessionIdUser", existinguser.UserId.ToString());

                    
                        TempData["SuccessMessage"] = "Logowanie zakończona sukcesem!";

                        return RedirectToAction("GoToAccount", "Home");
                    
                }
                else
                {
                    ModelState.AddModelError("Password", "Podaj poprawne hasło");
                    TempData["DangerMessage"] = "Wprowadż poprawne dane!";

                    return View("Login");
                }
            }
            else
            {
                TempData["DangerMessage"] = "Wprowadż poprawne dane!";
                ModelState.AddModelError("Username", "Podaj poprawny login");
                return View("Login");
            }
        }

    }
}
