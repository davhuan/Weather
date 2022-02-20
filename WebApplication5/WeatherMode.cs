using Newtonsoft.Json.Linq;

namespace WebApplication5
{
     class WeatherMode
    {
        public string type { get; set; }
        public JObject geometry { get; set; }
        public JObject properties { get; set; }

    }
}