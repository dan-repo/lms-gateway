using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LmsGateway.Core.Extensions
{
    public static class SessionExtensions
    {
        public static async Task<T> GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(data));
        }

        public static async Task SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, await Task.Factory.StartNew(() => JsonConvert.SerializeObject(value)));
        }

    }
}
