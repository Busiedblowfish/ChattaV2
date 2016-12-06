using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatta.Models.Chat
{
    /// <summary>
    /// ChatMessage derived from Chatta
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// Guestname of an authenticated user
        /// </summary>
        public string Username { get; set; }
  
        /// <summary>
        /// Chatroom message from a unique guestname
        /// </summary>
        public string Message { get; set; }     

        /// <summary>
        /// The local time message was delivered
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}