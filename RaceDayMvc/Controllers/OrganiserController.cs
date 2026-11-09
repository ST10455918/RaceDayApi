using Microsoft.AspNetCore.Mvc;
public class OrganiserController : Controller
{
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("Role") != "Organiser") return RedirectToAction("Login", "Account");
        return View();
    }
    public IActionResult CreateEvent() => View();
}