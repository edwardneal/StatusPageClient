using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class Incident
    {
        [JsonPropertyName("components")]
        public IEnumerable<Component> ComponentImpacts { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("impact")]
        public string Impact { get; set; }

        [JsonPropertyName("incident_updates")]
        public IEnumerable<IncidentUpdate> Updates { get; set; }

        [JsonPropertyName("monitoring_at")]
        public DateTimeOffset? MonitoringOn { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("page_id")]
        public string PageId { get; set; }

        [JsonPropertyName("resolved_at")]
        public DateTimeOffset? ResolvedOn { get; set; }

        [JsonPropertyName("shortlink")]
        public string Shortlink { get; set; }

        [JsonPropertyName("started_at")]
        public DateTimeOffset? StartedOn { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedOn { get; set; }
    }
}
