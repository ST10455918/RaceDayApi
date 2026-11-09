using Microsoft.AspNetCore.Mvc;
public class ParticipantController : Controller
{
    public IActionResult Index() => View();
    public IActionResult MyEnrolments() => View();
    public IActionResult MyResults() => View();
    public IActionResult Profile() => View();
}