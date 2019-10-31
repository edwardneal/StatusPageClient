using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class SummaryResponse
    {
        [JsonPropertyName("components")]
        public IEnumerable<Component> Components { get; set; }

        [JsonPropertyName("page")]
        public Page Page { get; set; }

        [JsonPropertyName("incidents")]
        public IEnumerable<Incident> Incidents { get; set; }

        [JsonPropertyName("scheduled_maintenances")]
        public IEnumerable<ScheduledMaintenance> ScheduledMaintenances { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }
}
