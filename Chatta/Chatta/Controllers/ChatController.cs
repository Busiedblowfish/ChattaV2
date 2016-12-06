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
        //Initialize the guest container to create a pool of connected guest
        public ChatController()
        {
            container = Memorize.GetInstance();
        }

        // GET: Chat
        [Authorize]     //Authenticate user, redirect to login page if not logged in
        public ActionResult Lobby()
        {
            return View();
        }

        [Authorize]     //Authenticate user, redirect to login page if not logged in
        [HttpPost]      //Tell the routing engine to send POST request following onClick "Join Chatroom"
        public ActionResult Lobby(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "Guestname is required");
                return View();
            }
            else
            {
                // Append random number to the username if already taken
                if (container.Users.Where(u => u.Username.Equals(username)).ToList().Count > 0)
                {
                    username = container.AppendRandToUsername(username);
                }
                return View("Chatroom", "_Layout", username);
            }
        }

        /* Authenticate user, redirect to login page if not logged in 
           If they haven't picked a guestname, redirected to the chatlobby view */ 
        [Authorize]     
        public ActionResult Chatroom(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return View("Lobby", "_Layout");
            }
            return View("Chatroom", "_Layout", username);

        }
    }
}