using System;

namespace NestedOptions.Api.Models
{
    public class User
    {
        public Guid UserId { get; private set; }
        public string Username { get; set; }
        public bool IsAdmin { get; private set; }
        public Preferences Preferences { get; private set; }
        public User(string username, bool isAdmin, Preferences preferences)
        {
            Username = username;
            IsAdmin = isAdmin;
            Preferences = preferences;
        }

        public void Update(string username, bool isAdmin, Preferences preferences)
        {
            Username = username;
            IsAdmin = isAdmin;
            Preferences = preferences;
        }

        private User()
        {

        }
    }
}
