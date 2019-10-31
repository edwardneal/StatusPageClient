using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class Status
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("indicator")]
        public string Indicator { get; set; }
    }
}
