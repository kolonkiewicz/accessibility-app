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
            using InzynierkaContext _context = new InzynierkaContext();

            var sessionIdUser = HttpContext.Session.GetString("SessionIdUser");
            int sessionIdUserToInt = int.Parse(sessionIdUser);
            
            var reports = _context.Scan
            .Include(s => s.Violations)
            .Where(s => s.UserId == sessionIdUserToInt)
            .OrderByDescending(s => s.ScanDate)
            .Select(s => new ReportListItemViewModel
            {
                ScanId = s.ScanId,
                Url = s.Url,
                ScanDate = s.ScanDate,
                ErrorCount = s.Violations.Count
            })
            .ToList();

            return View(reports);
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
