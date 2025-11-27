using inzynierka.Models;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using System.Text.Json;
using inzynierka.Models; 

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
        return RedirectToAction("Login", "Login");
    }

    public IActionResult GoToRegister()
    {
        return RedirectToAction("Register", "Register");
    }

    public IActionResult GoToAccount()
    {
        return RedirectToAction("Account", "Account");
    }

    public IActionResult GoToChangePassword()
    {
        return RedirectToAction("EditPassword", "Account");
    }

    public IActionResult GoToFile()
    {
        return RedirectToAction("File", "File");
    }

    public IActionResult GoToDashboard()
    {
        return RedirectToAction("Dashboard", "Dashboard");
    }

    public IActionResult GoToAddFile()
    {
        return RedirectToAction("FileAdd", "File");
    }


    [HttpPost]
    public async Task<IActionResult> Scan(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            ViewBag.Error = "Podaj poprawny adres URL.";
            return View("Index");
        }

        try
        {
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            });

            using var page = await browser.NewPageAsync();
            await page.GoToAsync(url, WaitUntilNavigation.Networkidle0);

            // 1️⃣ Wczytaj oba skrypty
            var axeScript = await System.IO.File.ReadAllTextAsync("wwwroot/js/axe.min.js");
            var axeLocale = await System.IO.File.ReadAllTextAsync("wwwroot/js/axe-locale-pl.js");

            // 2️⃣ Załaduj axe-core do strony
            await page.EvaluateExpressionAsync(axeScript);

            // 3️⃣ Wstrzyknij konfigurację języka PL (plik JSON → obiekt JS)
            await page.EvaluateExpressionAsync($"axe.configure({{ locale: {axeLocale} }});");

            // 4️⃣ Uruchom test z polską lokalizacją
            var resultsJson = await page.EvaluateFunctionAsync<string>("async () => JSON.stringify(await axe.run())");

            // 5️⃣ Odczytaj wyniki
            var doc = JsonDocument.Parse(resultsJson);
            var violationsJson = doc.RootElement.GetProperty("violations").GetRawText();

            var result = JsonSerializer.Deserialize<List<AxeViolation>>(violationsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            await browser.CloseAsync();
            return View("Index", result);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Index");
        }
    }
}
