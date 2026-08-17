using Microsoft.AspNetCore.Mvc;
using inzynierka.Models;

namespace inzynierka.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]

    public class ReportsApiController : ControllerBase
    {
        private readonly InzynierkaContext _context;

        public ReportsApiController(InzynierkaContext context)
        {
            _context = context;
        }

        // GET /api/ReportsApi
        [HttpGet]
        public IActionResult GetUserReports()
        {
            var sessionIdUser = HttpContext.Session.GetString("SessionIdUser");

            if (string.IsNullOrEmpty(sessionIdUser))
            {
                return Unauthorized();
            }

            int userId = int.Parse(sessionIdUser);

            var reports = _context.Scan
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.ScanDate)
                .Select(s => new
                {
                    scanId = s.ScanId,
                    url = s.Url,
                    scanDate = s.ScanDate,
                    errorCount = s.Violations.Count
                })
                .ToList();

            return Ok(reports);
        }
    }
}
