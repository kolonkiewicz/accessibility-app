using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;

namespace inzynierka.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitRegister(UserModel user)
        {
            using CloudContext cloudContext = new CloudContext();
            var existinglogin = cloudContext.Users.FirstOrDefault(u => u.Username == user.Username);
            var existingemail = cloudContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingemail == null)
            {
                if (existinglogin == null)
                {
                    user.Role = "user";
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            cloudContext.Add(user);
                            cloudContext.SaveChanges();
                            TempData["SuccessMessage"] = "Rejestracja zakończona sukcesem!";
                            return RedirectToAction("GoToLogin", "Home");

                        }
                        catch (Exception ex)
                        {
                            TempData["DangerMessage"] = "Cos poszlo nie tak";
                            return View("Register", user);
                        }
                    }
                    else
                    {
                        TempData["DangerMessage"] = "Uzupełnij dane";
                        return View("Register", user);
                    }
                }
                else
                {
                    TempData["DangerMessage"] = "Wprowadż poprawne dane!";
                    ModelState.AddModelError("Username", "Podany Username już istnieje");
                    return View("Register", user);
                }
            }
            else
            {
                TempData["DangerMessage"] = "Wprowadż poprawne dane!";
                ModelState.AddModelError("Email", "Email juz istnieje");
                return View("Register", user);
            }
            //TempData["SuccessMessage"] = "cos sie wyjebalo ";
            //return View("Register", user);
            //user.Role = "user";
            //if (ModelState.IsValid)
            //{
            //    TempData["SuccessMessage"] = "Rejestracja zakończona sukcesem!";
            //    return RedirectToAction("GoToLogin", "Home");
            //}
            //else
            //{

            //    TempData["SuccessMessage"] = "Rejestracja 6546546456456 sukcesem!";
            //    return View("Register", user);
            //}


        }


    }
}
