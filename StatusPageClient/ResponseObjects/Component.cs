using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class Component
    {
        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("group")]
        public bool Group { get; set; }

        [JsonPropertyName("group_id")]
        public string GroupId { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("only_show_if_degraded")]
        public bool OnlyShowIfDegraded { get; set; }

        [JsonPropertyName("page_id")]
        public string PageId { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("showcase")]
        public bool Showcase { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedOn { get; set; }

        [JsonPropertyName("components")]
        public IEnumerable<string> SubcomponentIds { get; set; }
    }
}
