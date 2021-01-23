using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CMSProject.Web.Models.Extensions
{
    public static class SessionExtention // Buraları sor...
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }

    }
}
