using inzynierka.Models;
using inzynierka.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inzynierka.Controllers
{
    public class ReportsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Declaration(int id)
        {
            using var db = new InzynierkaContext();

            var scan = db.Scan
                .Where(s => s.ScanId == id)
                .Select(s => new DeclarationViewModel
                {
                    Url = s.Url,
                    ScanDate = s.ScanDate,
                    Violations = s.Violations.ToList()
                })
                .FirstOrDefault();

            if (scan == null)
                return NotFound();

            return View(scan);
        }

    }
}
