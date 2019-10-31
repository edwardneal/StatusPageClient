using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class Page
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("time_zone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedOn { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
