using System;
using System.Collections.Generic;
using System.Text;

namespace StatusPageClient.Models
{
    /// <summary>
    /// A planned event relating to a status page and/or its subcomponents.
    /// </summary>
    public class ScheduledMaintenance : Incident, IEquatable<ScheduledMaintenance>
    {
        public DateTime? ScheduledStart { get; private set; }

        public DateTime? ScheduledEnd { get; private set; }

        internal ScheduledMaintenance(ResponseObjects.ScheduledMaintenance respSchedMaintenance, IEnumerable<Component> componentsLookup)
            : base(respSchedMaintenance, componentsLookup)
        {
            if (respSchedMaintenance.ScheduledStart.HasValue)
            {
                ScheduledStart = respSchedMaintenance.ScheduledStart.Value.UtcDateTime;
            }

            if (respSchedMaintenance.ScheduledEnd.HasValue)
            {
                ScheduledEnd = respSchedMaintenance.ScheduledEnd.Value.UtcDateTime;
            }
        }

        /// <summary>
        /// Returns true if the other <see cref="ScheduledMaintenance"/> instance has the same <see cref="Id"/> as this one.
        /// </summary>
        /// <param name="other">The other <see cref="ScheduledMaintenance"/> to compare.</param>
        /// <returns>True if the two objects match, false otherwise</returns>
        public bool Equals(ScheduledMaintenance other)
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
            return Equals(obj as ScheduledMaintenance);
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
