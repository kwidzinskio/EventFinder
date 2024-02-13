using Microsoft.AspNetCore.Mvc;
using SameZeraIJedynka.Controllers;
using SameZeraIjedynka.Database.Context;
using SameZeraIjedynka.Database.Entities;
using SameZeraIJedynka.BusinnessLogic.Models;
using Microsoft.EntityFrameworkCore;
using SameZeraIjedynka.Database.Repositories;
using SameZeraIjedynka.BusinnessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace SameZeraIJedynka.Controllers
{
    public class UserFavoriteController : Controller
    {
        private readonly IUserFavoriteService userFavoriteService;

        public UserFavoriteController(IUserFavoriteService userFavoriteService)
        {
            this.userFavoriteService = userFavoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOption = null)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                List<Event> events = await userFavoriteService.GetFavoriteEvents(userId.Value, sortOption);

                return View(events);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                await userFavoriteService.AddFavoriteEvent(id, userId.Value);
            }
            
            return RedirectToAction("Index", "UserFavorite");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                await userFavoriteService.DeleteFavoriteEvent(id, userId.Value);
            }

            return RedirectToAction("Index");
        }


    }
}


