using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatta.Models.Chat
{
    public class ChatMessage
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}