using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axon.Core.Configurations;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Axon.Business.Rules
{
    public static class BusinessRules
    {
        public static IConfiguration Configuration;
        public const int SRID = 4326;

        public static Guid GenerateIdentifier()
        {
            return Guid.NewGuid();
        }
        public static string CacheObjectKey(IdentifiedEntity obj)
        {
            return CacheObjectKey(obj.GetType(), obj.Id);
        }


        public static string CacheObjectKey(Type type, Guid id)
        {
            return $"{type.Name}-{id.ToString()}";
        }

        public static string CacheListKey(Type type)
        {
            return $"{type.Name}";
        }

        public static class Users
        {
            /// <summary>
            /// Generates a Random Password
            /// respecting the given strength requirements.
            /// </summary>
            /// <param name="opts">A valid PasswordOptions object
            /// containing the password strength requirements.</param>
            /// <returns>A random password</returns>
            public static string GenerateRandomPassword(PasswordOptions opts = null)
            {
                if (opts == null) opts = IdentityConfiguration.PasswordOpts;

                string[] randomChars = new[] {
        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
        "abcdefghijkmnopqrstuvwxyz",    // lowercase
        "0123456789",                   // digits
        "!@$?_-"                        // non-alphanumeric
    };
                Random rand = new Random(Environment.TickCount);
                List<char> chars = new List<char>();

                if (opts.RequireUppercase)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[0][rand.Next(0, randomChars[0].Length)]);

                if (opts.RequireLowercase)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[1][rand.Next(0, randomChars[1].Length)]);

                if (opts.RequireDigit)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[2][rand.Next(0, randomChars[2].Length)]);

                if (opts.RequireNonAlphanumeric)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[3][rand.Next(0, randomChars[3].Length)]);

                for (int i = chars.Count; i < opts.RequiredLength
                    || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
                {
                    string rcs = randomChars[rand.Next(0, randomChars.Length)];
                    chars.Insert(rand.Next(0, chars.Count),
                        rcs[rand.Next(0, rcs.Length)]);
                }

                return new string(chars.ToArray());
            }
        }

    }
}
