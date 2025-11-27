using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design.Serialization;

namespace inzynierka.Controllers
{
    public class AccountController : Controller
    {


        public IActionResult Account()
        {
            using CloudContext cloudContext = new CloudContext();
            var sessionIdUser = HttpContext.Session.GetString("SessionIdUser");
            ////int? userId = 1;

            if (sessionIdUser != null)
            {
                int sessionIdUserToInt = int.Parse(sessionIdUser);
                //TempData["SuccessMessage"] = sessionIdUser;
                var user = cloudContext.Users.FirstOrDefault(u => u.UserId == sessionIdUserToInt);
                return View(user);
            }
            else
            {
                return RedirectToAction("GoToLogin", "Home");
            }
        }

        
        public IActionResult EditPassword()
        {
                return View();
        }
        

        [HttpPost]
        public ActionResult SubmitChangePassword(UserModel user)
        {
            using CloudContext cloudContext = new CloudContext();


            var sessionIdUser = HttpContext.Session.GetString("SessionIdUser");
            

            if (ModelState["Password"].Errors.Count + ModelState["PPassword"].Errors.Count == 0  )
            {
                try
                {
                    int sessionIdUserToInt = int.Parse(sessionIdUser);
                    var user1 = cloudContext.Users.FirstOrDefault(u => u.UserId == sessionIdUserToInt);

                    user1.Password = user.Password;

                    cloudContext.SaveChanges();
                    TempData["SuccessMessage"] = "Poprawnie zmieniono hasło!";
                    return RedirectToAction("GoToAccount", "Home");

                }
                catch (Exception ex)
                {
                    TempData["DangerMessage"] = "Cos poszlo nie tak";
                    return View("EditPassword", user);
                }
            }
            else
            {
                TempData["DangerMessage"] = "Uzupełnij dane";
                return View("EditPassword", user);
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Wylogowano";
            return RedirectToAction("GoToLogin", "Home");
        }
    }


    
}
