using System;
using NestedOptions.Api.Models;

namespace NestedOptions.Api.Features
{
    public static class PreferencesExtensions
    {
        public static PreferencesDto ToDto(this Preferences preferences)
        {
            return new ()
            {
                PreferencesId = preferences.PreferencesId,
                AllowMultipleLanguages = preferences.AllowMultipleLanguages,
                AllowSocialSignIn = preferences.AllowSocialSignIn
            };
        }
        
    }
}
