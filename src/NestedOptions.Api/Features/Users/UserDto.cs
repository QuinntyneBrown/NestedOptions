using System;

namespace NestedOptions.Api.Features
{
    public class UserDto
    {
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public PreferencesDto Preferences { get; set; }
    }
}
