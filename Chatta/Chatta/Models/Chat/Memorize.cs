using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatta.Models.Chat
{
    public class Memorize
    {
        private static ICollection<ChatUser> connectedUsers;
        private static Dictionary<string, string> mappings;
        private static Memorize instance = null;
        private static readonly int maxRandom = 5;

        public static Memorize GetInstance()
        {
            if (instance == null)
            {
                instance = new Memorize();
            }
            return instance;
        }

        //Memorize the user by mapping to ICollection
        private Memorize()
        {
            connectedUsers = new List<ChatUser>();
            mappings = new Dictionary<string, string>();
        }

        //Connected User template
        public IQueryable<ChatUser> Users
        {
            get {
                    return connectedUsers.AsQueryable();
                }
        }

        //Add connected user  event handler for ChatUser Container
        public void Add(ChatUser user)
        {
            connectedUsers.Add(user);
        }

        //Remove connected user event handler for ChatUser Container
        public void Remove(ChatUser user)
        {
            connectedUsers.Remove(user);
        }

        /// <summary>
        /// Generate a random number and append to guestname if already taken
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public string AppendRandToUsername(string newUser)
        {
            string randUsername = newUser;
            int newRandom = maxRandom;
            int oldRandom = 0;
            int counter = 0;

            Random random = new Random(); 
            do
            {
                if (counter > newRandom)
                {
                    oldRandom = newRandom;
                    newRandom *= 2;
                }
                newUser = "@"+ randUsername + random.Next(oldRandom, newRandom).ToString();
                counter++;
            } while (GetInstance().Users.Where(u => u.Username.Equals(newUser)).ToList().Count > 0);

            return newUser;
        }

        public void AddMapping(string connectionId, string userId)
        {
            if (!string.IsNullOrEmpty(connectionId) && !string.IsNullOrEmpty(userId))
            {
                //Map a userId to their respective connectionId
                mappings.Add(connectionId, userId);
            }
        }

        /// <summary>
        /// Map a connection ID to its guestname
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public string GetUserByConnectionId(string connectionId)
        {
            string userId = null;
            mappings.TryGetValue(connectionId, out userId);
            return userId;
        }
    }
}