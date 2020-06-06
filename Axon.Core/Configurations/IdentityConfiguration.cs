using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Axon.Core.Configurations
{
    public static class IdentityConfiguration
    {
        public static Action<IdentityOptions> IdentityOpts = opt =>
        {
            opt.Password = PasswordOpts;

            // Lockout settings.
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.AllowedForNewUsers = true;

            // User settings.
            opt.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            opt.User.RequireUniqueEmail = true;
            opt.SignIn.RequireConfirmedEmail = true;
        };

        public static PasswordOptions PasswordOpts
        {
            get
            {
                return new PasswordOptions
                {
                    // Password settings.
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = false,
                    RequiredLength = 6,
                    RequiredUniqueChars = 1
                };
            }
        }
    }
}
