using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SameZeraIjedynka.Database.Entities;
using SameZeraIjedynka.Database.Context;
using SameZeraIJedynka.BusinnessLogic.Models;

using SameZeraIjedynka.BusinnessLogic.Services;
using SameZeraIJedynka.Models;
using SameZeraIjedynka.BusinnessLogic.Models;
using SameZeraIJedynka.BusinnessLogic.Services;

namespace SameZeraIJedynka.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IEventService eventService;

        public UsersController(IUserService userService, IEventService eventService)  
        {
            this.userService = userService;
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                var user = await userService.GetUserModelById(userId.Value);

                if (user != null)
                {
                    return View(user);
                }
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserModel model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                var user = await userService.GetUserModelById(userId.Value);

                if (user != null)
                {
                    await userService.UpdateUser(user, model);
                    // TODO: Show User That Data Has Been Updated
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel user)
        {
            if (ModelState.IsValid)
            {
                if (await userService.IsUsernameUnique(user.Name))
                {
                    await userService.AddUser(user);
                    await userService.SendEmail(user);

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Name", "Użytkownik o podanej nazwie już istnieje.");
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                bool isAuthenticated = await userService.AuthenticateUser(model);

                if (isAuthenticated)
                {
                    var userId = await userService.GetUserId(model);
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    HttpContext.Session.SetInt32("UserId", userId);
                    return RedirectToAction("Index", new { id = userId });
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub hasło.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("IsLoggedIn");
            HttpContext.Session.Remove("UserId");

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> MyEvents()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                var events = await eventService.GetEventsForUser(userId.Value);

                return View(events);
                
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEvent(int eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            bool belongsToUser = await eventService.EventBelongsToUser(userId.Value, eventId);
            if (!belongsToUser)
            {
                return RedirectToAction("EventDetails", "Event", new { id = eventId });
            }

            var events = await eventService.EventDetails(eventId);
            var eventModel = await eventService.ConvertEventToEventModel(events);

            return View(eventModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent(EventModel addEventRequest, IFormFile image)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                await eventService.Update(addEventRequest, image, userId.Value);

                return RedirectToAction("EventDetails", "Event", new { id = addEventRequest.EventId });
            }

            return RedirectToAction("Login", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                await eventService.Delete(eventId);

                return RedirectToAction("MyEvents");
            }

            return RedirectToAction("Login", "Users");
        }

    }
}

