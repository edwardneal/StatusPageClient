using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class ScheduledMaintenanceResponse
    {
        [JsonPropertyName("page")]
        public Page Page { get; set; }

        [JsonPropertyName("scheduled_maintenances")]
        public IEnumerable<ScheduledMaintenance> ScheduledMaintenances { get; set; }
    }
}
