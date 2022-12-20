using Newtonsoft.Json;

namespace GraphicInterface.Common
{
    public static class Extensions
    {
        public static T? Get<T>(this ISession session, string key)
        {
            var json = session.GetString(key);

            if (json is null) return default;

            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public static void Set(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}