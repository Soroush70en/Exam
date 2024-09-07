using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IoC
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Include)]
    public class ResponseBody<T> : ResponseBody
    {
        public new T Data { get; set; }
    }

    public class ResponseBody
    {
        public object Data { get; set; }

        public string Message { get; set; }

        public bool Status { get; set; }
    }
}
