namespace SameZeraIJedynka.Views.Shared.Components.Navbar
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            bool isLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";

            return View(isLoggedIn);
        }
    }
}
