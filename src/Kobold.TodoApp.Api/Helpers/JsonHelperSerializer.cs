using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kobold.TodoApp.Api.Helpers
{
    public class JsonHelperSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonHelperSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
        }

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, _settings);
        }

        public object DeserializeObject(string value)
        {
            return JsonConvert.DeserializeObject(value, _settings);
        }
    }
}