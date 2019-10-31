using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StatusPageClient.ResponseObjects
{
    internal class IncidentUpdate
    {
        [JsonPropertyName("affected_components")]
        public IEnumerable<ComponentImpact> ComponentImpacts { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonPropertyName("custom_tweet")]
        public string CustomTweet { get; set; }

        [JsonPropertyName("deliver_notifications")]
        public bool Notify { get; set; }

        [JsonPropertyName("display_at")]
        public DateTimeOffset? DisplayOn { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("incident_id")]
        public string IncidentId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("tweet_id")]
        public decimal? TweetId { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedOn { get; set; }
    }
}
