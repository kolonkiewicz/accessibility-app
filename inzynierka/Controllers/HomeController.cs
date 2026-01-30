using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using inzynierka.Models.ViewModels;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult GoToLogin()
    {
        return RedirectToAction("Index", "Login");
    }

    public IActionResult GoToResetPassword()
    {
        return RedirectToAction("Index", "ResetPassword");
    }

    public IActionResult GoToRegister()
    {
        return RedirectToAction("Index", "Register");
    }

    public IActionResult GoToAccount()
    {
        return RedirectToAction("Index", "Account");
    }

    public IActionResult GoToChangePassword()
    {
        return RedirectToAction("EditPassword", "Account");
    }

    public IActionResult GoToReports()
    {
        return RedirectToAction("Index", "Reports");
    }

    public IActionResult GoToDashboard()
    {
        return RedirectToAction("Dashboard", "Dashboard");
    }

    [HttpPost]
    public async Task<IActionResult> Scan(string url)
    {
        using InzynierkaContext _context = new InzynierkaContext();

        var sessionIdUser = HttpContext.Session.GetString("SessionIdUser");
        int sessionIdUserToInt = int.Parse(sessionIdUser);

        if (string.IsNullOrWhiteSpace(url))
        {
            TempData["DangerMessage"] = "Podaj poprawny adres URL.";
            return View("Index");
        }

        try
        {
            // 1. Uruchomienie przeglądarki
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            });

            using var page = await browser.NewPageAsync();
            await page.GoToAsync(url, WaitUntilNavigation.Networkidle0);

            // 2. Załadowanie axe-core
            var axeScript = await System.IO.File.ReadAllTextAsync("wwwroot/js/axe.min.js");
            var axeLocale = await System.IO.File.ReadAllTextAsync("wwwroot/js/axe-locale-pl.js");

            await page.EvaluateExpressionAsync(axeScript);
            await page.EvaluateExpressionAsync($"axe.configure({{ locale: {axeLocale} }});");

            // 3. Uruchomienie testów
            var resultsJson = await page.EvaluateFunctionAsync<string>("async () => JSON.stringify(await axe.run())");

            await browser.CloseAsync();

            // 4. Deserializacja JSON → AxeResult
            var axeResult = JsonSerializer.Deserialize<AxeResult>(resultsJson);

            // 5. Zapis do bazy danych
            var scan = new ScanModel
            {
                Url = url,
                FullResultJson = resultsJson,
                UserId = sessionIdUserToInt 
            };

            _context.Scan.Add(scan);
            await _context.SaveChangesAsync();

            // 6. Zapis błędów
            foreach (var v in axeResult.Violations)
            {
                foreach (var node in v.Nodes)
                {
                    var violation = new ViolationModel
                    {
                        ScanId = scan.ScanId,
                        RuleId = v.Id,
                        Impact = v.Impact,
                        Description = v.Description,
                        Help = v.HelpUrl,
                        Selector = string.Join(", ", node.Target),
                        Html = node.Html
                    };

                    _context.Vialations.Add(violation);
                }
            }

            await _context.SaveChangesAsync();

            // 7. Przekierowanie do wyników
            return RedirectToAction("ScanDetails", new { id = scan.ScanId });
        }
        catch 
        {
            TempData["DangerMessage"] = "Podaj poprawny adres URL.";
            return View("Index");
        }
    }

    // *************** WIDOK KONKRETNEGO SKANU *********************

    public IActionResult ScanDetails(int id)
    {
        using InzynierkaContext _context = new InzynierkaContext();
        var scan = _context.Scan
            .Include(s => s.Violations)
            .Where(s => s.ScanId == id)
            .Select(s => new ScanResultViewModel
            {
                ScanId = s.ScanId,
                Url = s.Url,
                Date = s.ScanDate,
                Violations = s.Violations
                    .Select(v => new ScanViolationWithFix
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
            return NotFound();

        return View(scan);
    }

}
