using Microsoft.EntityFrameworkCore;
using System;

namespace NestedOptions.Api.Models
{
    [Owned]
    public class Preferences
    {
        public Guid PreferencesId { get; private set; }
        public bool AllowSocialSignIn { get; private set; }
        public bool AllowMultipleLanguages { get; private set; }

        public Preferences(bool allowSocialSignIn, bool allowMultipleLanguages)
        {
            AllowSocialSignIn = allowSocialSignIn;
            AllowMultipleLanguages = allowMultipleLanguages;
        }

        private Preferences()
        {

        }
    }
}
