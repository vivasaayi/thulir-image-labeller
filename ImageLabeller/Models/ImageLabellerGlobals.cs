using System.Text.Json.Serialization;

namespace ImageLabeller.Models
{
    public class ImageLabellerGlobals
    {
        [JsonPropertyName("openWeatherAppKey")]
        public string OpenWeatherAppKey { get; set; }
        
        [JsonPropertyName("elasticsearchurl")]
        public string ElasticSearchUrl { get; set; }
        
        [JsonPropertyName("postgresHost")]
        public string PostgresHost { get; set; }
        
        [JsonPropertyName("postgresUserName")]
        public string PostgresUserName { get; set; }
        
        [JsonPropertyName("postgresPassword")]
        public string PostgresPassword { get; set; }
        
        [JsonPropertyName("postgresPort")]
        public string PostgresPort { get; set; }
        
        [JsonPropertyName("postgresDatabase")]
        public string PostgresDatabase { get; set; }
        
        [JsonPropertyName("rawdatasetpath")]
        public string RawDataSetPath { get; set; }
    }
}