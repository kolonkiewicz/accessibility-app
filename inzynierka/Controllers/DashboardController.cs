using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inzynierka.Controllers
{
    public class DashboardController : Controller
    {
        public async Task <IActionResult> Dashboard()
        {
            using CloudContext cloudContext = new CloudContext();

            var users = await cloudContext.Users.ToListAsync();

            return View(users); 
            
        }

        public async Task<IActionResult> Details(int? id)
        {

            using CloudContext cloudContext = new CloudContext();

            if (id == null || cloudContext.Users == null)
            {
                return NotFound();
            }

            var userModel = await cloudContext.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            using CloudContext cloudContext = new CloudContext();
            if (id == null || cloudContext.Users == null)
            {
                return NotFound();
            }

            var userModel = await cloudContext.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,UserModel user)
        {
            using CloudContext cloudContext = new CloudContext();
            var sessionIdUser = HttpContext.Session.GetString("SessionIdUser");


            if (ModelState["Role"]  != null)
            {
                if (user.Role == "admin" || user.Role == "user")
                {
                    try
                    {
                        var user1 = cloudContext.Users.FirstOrDefault(u => u.UserId == id);

                        user1.Role = user.Role;

                        cloudContext.SaveChanges();
                        TempData["SuccessMessage"] = "Poprawnie zmieniono hasło!";
                        return RedirectToAction("GoToDashboard", "Home");

                    }
                    catch (Exception ex)
                    {
                        TempData["DangerMessage"] = "Cos poszlo nie tak";
                        return View("Edit", user);
                    }
                }
                else
                {
                    ModelState.AddModelError("Role", "Dostepne role to user i admin");
                    TempData["DangerMessage"] = "Uzupełnij poprawnie dane";
                    return View("Edit", user);
                }
            }
            else
            {
                ModelState.AddModelError("Role", "Podaj role");
                TempData["DangerMessage"] = "Uzupełnij dane";
                return View("Edit", user);
            }
        }

        

    }
}
