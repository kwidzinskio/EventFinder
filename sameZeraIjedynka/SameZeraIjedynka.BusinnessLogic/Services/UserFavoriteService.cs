using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SameZeraIJedynka.BusinnessLogic.Models;
using Microsoft.EntityFrameworkCore;
using SameZeraIjedynka.Database.Context;
using SameZeraIjedynka.Database.Entities;
using SameZeraIjedynka.Database.Repositories;

namespace SameZeraIjedynka.BusinnessLogic.Services
{
    public class UserFavoriteService : IUserFavoriteService
    {
        private readonly IUserFavoriteRepository userFavoriteRepository;

        public UserFavoriteService(IUserFavoriteRepository userFavoriteRepository)
        {
            this.userFavoriteRepository = userFavoriteRepository;
        }

        public async Task<List<Event>> GetFavoriteEvents(int userId, string sortOption = null)
        {
            var eventsQuery = await userFavoriteRepository.Get(userId);

            switch (sortOption)
            {
                case "time_left":
                    eventsQuery = eventsQuery.OrderBy(e => e.Date);
                    break;
                case "time_left_desc":
                    eventsQuery = eventsQuery.OrderByDescending(e => e.Date);
                    break;
                case "price":
                    eventsQuery = eventsQuery.OrderBy(e => e.Price);
                    break;
                case "price_desc":
                    eventsQuery = eventsQuery.OrderByDescending(e => e.Price);
                    break;
                default:
                    eventsQuery = eventsQuery;
                    break;
            }
            var events = await eventsQuery.ToListAsync();

            return events;
        }

        public async Task AddFavoriteEvent(int id, int userId)
        {
            var newFavorite = new UserFavorite()
            {
                EventId = id,
                UserId = userId
            };

            await userFavoriteRepository.Add(newFavorite);
        }

        public async Task DeleteFavoriteEvent(int id, int userId)
        {
            var newFavorite = new UserFavorite()
            {
                EventId = id,
                UserId = userId
            };

            var favoriteEvent = await userFavoriteRepository.Find(id, userId);

            if (favoriteEvent != null)
            {
                await userFavoriteRepository.Delete(favoriteEvent);
            }
        }
    }
}
