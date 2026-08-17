using Microsoft.AspNetCore.Mvc;
using inzynierka.Models;
using Microsoft.EntityFrameworkCore;

namespace inzynierka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AllScansApiController : ControllerBase
    {
        private readonly InzynierkaContext _context;

        public AllScansApiController(InzynierkaContext context)
        {
            _context = context;
        }

        // GET api/all/scans
        [HttpGet]
        public IActionResult GetAllScans()
        {
            var scans = _context.Scan
                .Select(s => new
                {
                    scanID = s.ScanId,
                    url = s.Url,
                    scanDate = s.ScanDate,
                    userId = s.UserId,
                    violationsCount = s.Violations.Count()
                })
                .OrderByDescending(s => s.scanDate).ToList();

            return Ok(scans);
        }

        // GET /api/allscans/id
        [HttpGet("{id}")]
        public IActionResult GetScanDetails(int id)
        {
            var scan = _context.Scan
                .Where(s => s.ScanId == id)
                .Select(s => new
                {
                    scanID = s.ScanId,
                    url = s.Url,
                    scanDate = s.ScanDate,
                    userId = s.UserId,
                    violations = s.Violations
                        .Select(v => new
                        {
                            Violation = v,
                            Suggestion = _context.FixSuggestions
                            .Where(f => f.RuleId == v.RuleId)
                            .Select(f => f.Suggestion)
                            .FirstOrDefault() ?? "Brak rekomendacji"
                        }).ToList()
                })
                .FirstOrDefault();
            if (scan == null)
            {
                return NotFound();
            }
            return Ok(scan);
        }
    }
}