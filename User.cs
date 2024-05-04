using System.Text.Json.Serialization;

namespace REST_API_APP_for_parameter
{
    public enum TableGenre { Male, Female }
    public class User
    {
        public string ID { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TableGenre Genre { get; set; }
        public Parameter? Params { get; set; }
    }
}
