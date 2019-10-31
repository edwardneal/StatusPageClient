using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class IncidentsResponse
    {
        [JsonPropertyName("page")]
        public Page Page { get; set; }

        [JsonPropertyName("incidents")]
        public IEnumerable<Incident> Incidents { get; set; }
    }
}
