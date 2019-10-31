using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class ScheduledMaintenance : Incident
    {
        [JsonPropertyName("scheduled_for")]
        public DateTimeOffset? ScheduledStart { get; set; }

        [JsonPropertyName("scheduled_until")]
        public DateTimeOffset? ScheduledEnd { get; set; }
    }
}
