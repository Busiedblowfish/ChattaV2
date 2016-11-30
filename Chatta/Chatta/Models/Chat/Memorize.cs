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

        private Memorize()
        {
            connectedUsers = new List<ChatUser>();
            mappings = new Dictionary<string, string>();
        }

        public IQueryable<ChatUser> Users { get { return connectedUsers.AsQueryable(); } }

        public void Add(ChatUser user)
        {
            connectedUsers.Add(user);
        }

        public void Remove(ChatUser user)
        {
            connectedUsers.Remove(user);
        }

        #region Generate a random number and append to usernames if already taken
        public string AppendRandToUsername(string newUser)
        {
            string tempUsername = newUser;
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
                newUser = tempUsername + random.Next(oldRandom, newRandom).ToString();
                counter++;
            } while (GetInstance().Users.Where(u => u.Username.Equals(newUser)).ToList().Count > 0);

            return newUser;
        }
        #endregion

        public void AddMapping(string connectionId, string userId)
        {
            if (!string.IsNullOrEmpty(connectionId) && !string.IsNullOrEmpty(userId))
            {
                //Map a userId to their respective connectionId
                mappings.Add(connectionId, userId);
            }
        }

        public string GetUserByConnectionId(string connectionId)
        {
            string userId = null;
            mappings.TryGetValue(connectionId, out userId);
            return userId;
        }
    }
}