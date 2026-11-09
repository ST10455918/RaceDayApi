using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _http;
    public AccountController(IHttpClientFactory http) => _http = http;

    [HttpGet] public IActionResult Login() => View();
    [HttpGet] public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string Email, string Password, string Role)
    {
        var client = _http.CreateClient("RaceDayApi");
        var json = JsonSerializer.Serialize(new { Email, Password, Role });
        var res = await client.PostAsync("/api/auth/register", new StringContent(json, Encoding.UTF8, "application/json"));
        if (res.IsSuccessStatusCode) return RedirectToAction("Login");
        ViewBag.Error = "Email already exists";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string Email, string Password)
    {
        var client = _http.CreateClient("RaceDayApi");
        var json = JsonSerializer.Serialize(new { Email, Password });
        var res = await client.PostAsync("/api/auth/login", new StringContent(json, Encoding.UTF8, "application/json"));
        if (!res.IsSuccessStatusCode) { ViewBag.Error = "Invalid login"; return View(); }
        var content = await res.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        HttpContext.Session.SetInt32("UserId", user!.Id);
        HttpContext.Session.SetString("Role", user.Role);
        HttpContext.Session.SetString("Email", user.Email);
        if (user.Role == "Organiser") return RedirectToAction("Dashboard", "Organiser");
        return RedirectToAction("Index", "Participant");
    }

    public IActionResult Logout() { HttpContext.Session.Clear(); return RedirectToAction("Login"); }
    record UserDto(int Id, string Email, string Role);
}