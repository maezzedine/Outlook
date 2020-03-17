using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using backend.Areas.Identity;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly UserManager<OutlookUser> userManager;

        public NotificationsController(OutlookContext context, UserManager<OutlookUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var notifications = from notification in context.Notification
                                where notification.UserID == user.Id
                                select notification;

            return await notifications.ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await context.Notification.FindAsync(id);

            var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (notification == null || notification.UserID != user.Id)
            {
                return NotFound();
            }

            return notification;
        }
    }
}
