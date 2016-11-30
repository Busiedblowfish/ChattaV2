using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chatta.Models.Chat;

namespace Chatta.Controllers
{
    public class ChatController : Controller
    {
        private Memorize container;

        public ChatController()
        {
            container = Memorize.GetInstance();
        }

        // GET: Chat
        [Authorize]     //Authenticate user, redirect to login page if not logged in
        public ActionResult Chatroom()
        {
            return View();
        }

    
        [Authorize]     //Authenticate user, redirect to login page if not logged in
        public ActionResult Lobby(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "Username is required");
                return View();
            }
            else
            {
                // Append random number to the username if already taken
                if (container.Users.Where(u => u.Username.Equals(username)).ToList().Count > 0)
                {
                    username = container.AppendRandToUsername(username);
                }
                return View("Lobby", "_Layout", username);
            }
        }
    }
}