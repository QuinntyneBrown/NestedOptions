using System;
using NestedOptions.Api.Models;

namespace NestedOptions.Api.Features
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this User user)
        {
            return new ()
            {
                UserId = user.UserId,
                Username = user.Username,
                IsAdmin = user.IsAdmin,
                Preferences = user.Preferences.ToDto()
            };
        }
        
    }
}
