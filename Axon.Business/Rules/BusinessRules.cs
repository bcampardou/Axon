using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities.Base;
using Microsoft.Extensions.Configuration;

namespace Axon.Business.Rules
{
    public static class BusinessRules
    {
        public static IConfiguration Configuration;
        public const int SRID = 4326;

        public static string GenerateIdentifier()
        {
            return Guid.NewGuid().ToString();
        }
        public static string CacheObjectKey(IdentifiedEntity obj)
        {
            return CacheObjectKey(obj.GetType(), obj.Id);
        }

        public static string CacheObjectKey(Type type, string id)
        {
            return $"{type.Name}-{id}";
        }

        public static string CacheListKey(Type type)
        {
            return $"{type.Name}";
        }
    }
}
