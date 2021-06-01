using System;

namespace NestedOptions.Api.Features
{
    public class PreferencesDto
    {
        public Guid? PreferencesId { get; set; }
        public bool AllowSocialSignIn { get; set; }
        public bool AllowMultipleLanguages { get; set; }
    }
}
