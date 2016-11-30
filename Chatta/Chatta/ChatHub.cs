using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Chatta.Models.Chat;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Chatta
{
    public class ChatHub : Hub
    {
            /* For testing message broadcast to all connected clients
            public void Send(string email, string message)
            {
                //Broadcast the message to all clients including self
                Clients.All.broadcastMessage(email, message);
            }
            */

        private Memorize container;

        public ChatHub()
        {
            container = Memorize.GetInstance();
        }

        #region IDisconnect and IConnected event handlers implementation

        /// <summary>
        /// Fired when a client disconnects from the system. The Username associated with the ConnectionID gets deleted from the list of currently connected users.
        /// </summary>
        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = container.GetUserByConnectionId(Context.ConnectionId);
            if (userId != null)
            {
                ChatUser user = container.Users.Where(u => u.ConnectionID == userId).FirstOrDefault();
                if (user != null)
                {
                    container.Remove(user);
                    return Clients.All.leaves(user.ConnectionID, user.Username, DateTime.Now);
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        //Extract Url links from chat messages using regex
        public string TextParser(string url)
        {
            url = Regex.Replace(url, @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)",
                                    "<a target='_blank' href='$1'>$1</a>");
            return url;
        }
        #endregion

        #region Chat event handlers

        /// <summary>
        /// Fired when a client send a message to the server.
        /// </summary>
        /// <param name="message"></param>
        public void Send(ChatMessage Content)
        {
            if (!string.IsNullOrEmpty(Content.Message))
            {
                // Sanitize input
                Content.Message = HttpUtility.HtmlEncode(Content.Message);
                // Process URLs: Extract any URL
                Content.Message = TextParser(Content.Message);
                Content.Timestamp = DateTime.Now;
                Clients.All.onMessageReceived(Content);
            }
        }

        /// <summary>
        /// Fired when a client joins the chat. Here round trip state is available and we can register the user in the list
        /// </summary>
        public void Joined()
        {
            //Create an instance of ChatUser class 
            ChatUser user = new ChatUser()
            {
                //Id = Context.ConnectionId,                
                ConnectionID = Guid.NewGuid().ToString(),
                Username = Clients.Caller.username
            };
            //Map each Username to their ConnectionId
            container.Add(user);
            container.AddMapping(Context.ConnectionId, user.ConnectionID);
            Clients.All.joins(user.Username, user.ConnectionID, DateTime.Now);
        }

        /// <summary>
        /// Invoked when a client connects. Retrieves the list of all currently connected users
        /// </summary>
        /// <returns></returns>
        public ICollection<ChatUser> GetConnectedUsers()
        {
            //Retrieve the list of connected users
            return container.Users.ToList();
        }
        #endregion
    }
}
