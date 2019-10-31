using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StatusPageClient.Models
{
    /// <summary>
    /// A change in state for a <see cref="Component"/> associated with an <see cref="IncidentUpdate"/>.
    /// </summary>
    public class ComponentStatusChange : IEquatable<ComponentStatusChange>
    {
        /// <summary>
        /// The <see cref="Component"/> whose status is changing.
        /// </summary>
        public Component Component { get; private set; }

        /// <summary>
        /// The friendly name of this status change.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The former status of this <see cref="Component"/>.
        /// </summary>
        public StatusDescription OldStatus { get; private set; }

        /// <summary>
        /// The status which this <see cref="Component"/> has transitioned to.
        /// </summary>
        public StatusDescription NewStatus { get; private set; }

        internal ComponentStatusChange(ResponseObjects.ComponentImpact respComponentImpact, IEnumerable<Component> componentsLookup)
        {
            Name = respComponentImpact.Name;
            OldStatus = new StatusDescription(respComponentImpact.OldStatus, null);
            NewStatus = new StatusDescription(respComponentImpact.NewStatus, null);

            Component = componentsLookup.FirstOrDefault(c => string.Equals(c.Id, respComponentImpact.ComponentId, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Component}: {OldStatus} -> {NewStatus}";
        }

        /// <summary>
        /// Returns true if the other <see cref="ComponentStatusChange"/> instance has the same <see cref="Component"/>, <see cref="OldStatus"/> and <see cref="NewStatus"/> as this one.
        /// </summary>
        /// <param name="other">The other <see cref="ComponentStatusChange"/> to compare.</param>
        /// <returns>True if the two objects match, false otherwise</returns>
        public bool Equals(ComponentStatusChange other)
        {
            return other != null && other.Component == Component && other.OldStatus == OldStatus && other.NewStatus == NewStatus;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ComponentStatusChange);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
