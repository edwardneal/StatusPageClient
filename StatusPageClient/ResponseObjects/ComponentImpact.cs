using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class ComponentImpact
    {
        [JsonPropertyName("code")]
        public string ComponentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("new_status")]
        public string NewStatus { get; set; }

        [JsonPropertyName("old_status")]
        public string OldStatus { get; set; }
    }
}
