using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StatusPageClient.Models
{
    /// <summary>
    /// An event relating to a status page and/or its subcomponents.
    /// </summary>
    public class Incident : IEquatable<Incident>
    {
        /// <summary>
        /// The unique identifier of this incident.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// A friendly name for this incident.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The URL for this incident.
        /// </summary>
        public string Shortlink { get; private set; }

        /// <summary>
        /// The impact this incident had upon <see cref="ImpactedComponents"/>.
        /// </summary>
        public string Impact { get; private set; }

        /// <summary>
        /// UTC date that this incident was created.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// UTC date that this incident was last updated.
        /// </summary>
        public DateTime UpdatedOn { get; private set; }

        /// <summary>
        /// If present, the UTC date that this incident began to impact <see cref="ImpactedComponents"/>.
        /// </summary>
        public DateTime? StartedOn { get; private set; }

        /// <summary>
        /// If present, the UTC date that this incident was resolved.
        /// </summary>
        public DateTime? ResolvedOn { get; private set; }

        /// <summary>
        /// Current status of this incident.
        /// </summary>
        public StatusDescription Status { get; private set; }

        /// <summary>
        /// All updates/changes of status for this incident.
        /// </summary>
        public IEnumerable<IncidentUpdate> Updates { get; private set; }

        /// <summary>
        /// All components impacted by this incident.
        /// </summary>
        public IEnumerable<Component> ImpactedComponents { get; private set; }

        internal Incident(ResponseObjects.Incident respIncident, IEnumerable<Component> componentsLookup)
        {
            Id = respIncident.Id;
            Name = respIncident.Name;
            Shortlink = respIncident.Shortlink;
            Impact = respIncident.Impact;

            CreatedOn = respIncident.CreatedOn.UtcDateTime;
            UpdatedOn = respIncident.UpdatedOn.UtcDateTime;

            if (respIncident.StartedOn.HasValue)
            {
                StartedOn = respIncident.StartedOn.Value.UtcDateTime;
            }
            if (respIncident.ResolvedOn.HasValue)
            {
                ResolvedOn = respIncident.ResolvedOn.Value.UtcDateTime;
            }

            Status = new StatusDescription(respIncident.Status, null);

            if (respIncident.ComponentImpacts != null)
            {
                var componentDictionary = componentsLookup.ToDictionary(c => c.Id);

                ImpactedComponents = (from ic in respIncident.ComponentImpacts
                                      where componentDictionary.ContainsKey(ic.Id)
                                      select componentDictionary[ic.Id]).ToArray();
            }

            if (respIncident.Updates != null)
            {
                Updates = respIncident.Updates.Select(iu => new IncidentUpdate(iu, componentsLookup)).ToArray();
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Name} ({Id})";
        }

        /// <summary>
        /// Returns true if the other <see cref="Incident"/> instance has the same <see cref="Id"/> as this one.
        /// </summary>
        /// <param name="other">The other <see cref="Incident"/> to compare.</param>
        /// <returns>True if the two objects match, false otherwise</returns>
        public bool Equals(Incident other)
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
            return Equals(obj as Incident);
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
