using System;

namespace DynamicVNET.Lib.AspNetCore
{
    [System.AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public class ProfileBind : Attribute
    {
        public string ProfileName { get; }

        public ProfileBind(string profileName)
        {
            if (string.IsNullOrEmpty(profileName))
                throw new ArgumentNullException(nameof(profileName));
            this.ProfileName = profileName;
        }
    }
}
