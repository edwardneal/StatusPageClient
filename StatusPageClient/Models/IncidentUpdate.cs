using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StatusPageClient.Models
{
    /// <summary>
    /// An additional update made to an incident (with associated impact and status change.)
    /// </summary>
    public class IncidentUpdate : IEquatable<IncidentUpdate>
    {
        /// <summary>
        /// The unique identifier of this incident update.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// A human-readable description of this incident update.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// The UTC time which this incident update was created at.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// The UTC time of this incident update's last change.
        /// </summary>
        public DateTime UpdatedOn { get; private set; }

        /// <summary>
        /// The new status of the associated incident.
        /// </summary>
        public StatusDescription Status { get; private set; }

        /// <summary>
        /// If this incident update was posted on Twitter, the ID of the tweet.
        /// </summary>
        public decimal? TweetId { get; private set; }

        /// <summary>
        /// Any component status changes which occurred as a result of this incident update.
        /// </summary>
        public IEnumerable<ComponentStatusChange> ComponentStatusChanges { get; private set; }

        internal IncidentUpdate(ResponseObjects.IncidentUpdate respIncidentUpdate, IEnumerable<Component> componentsLookup)
        {
            Id = respIncidentUpdate.Id;
            Body = respIncidentUpdate.Body;
            CreatedOn = respIncidentUpdate.CreatedOn.UtcDateTime;
            UpdatedOn = respIncidentUpdate.UpdatedOn.UtcDateTime;
            Status = new StatusDescription(respIncidentUpdate.Status, null);
            TweetId = respIncidentUpdate.TweetId;

            if (respIncidentUpdate.ComponentImpacts != null)
            {
                ComponentStatusChanges = respIncidentUpdate.ComponentImpacts.Select(ci => new ComponentStatusChange(ci, componentsLookup)).ToArray();
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{CreatedOn.ToString("F")}: {Body}";
        }

        /// <summary>
        /// Returns true if the other <see cref="IncidentUpdate"/> instance has the same <see cref="Id"/> as this one.
        /// </summary>
        /// <param name="other">The other <see cref="IncidentUpdate"/> to compare.</param>
        /// <returns>True if the two objects match, false otherwise</returns>
        public bool Equals(IncidentUpdate other)
        {
            return other != null && string.Equals(Id, other.Id, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IncidentUpdate);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
