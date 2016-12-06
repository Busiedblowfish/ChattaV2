using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatta.Models.Chat
{
    /// <summary>
    /// ChatUser derived from Chatta
    /// </summary>
    public class ChatUser
    {
        /// <summary>
        /// Unique connection ID for a guestname
        /// </summary>
        public string ConnectionID { get; set; }

        /// <summary>
        /// The guestname assigned to a specific user mapped with their connection ID
        /// </summary>
        public string Username { get; set; }
    }
}