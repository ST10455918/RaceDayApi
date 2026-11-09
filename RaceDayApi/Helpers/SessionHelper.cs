namespace RaceDayApi.Helpers
{
    public static class SessionHelper
    {
        public static int? GetUserId(HttpContext http) => http.Session.GetInt32("UserId");
        public static string? GetRole(HttpContext http) => http.Session.GetString("Role");
        public static bool IsOrganiser(HttpContext http) => GetRole(http) == "Organiser";
    }
}