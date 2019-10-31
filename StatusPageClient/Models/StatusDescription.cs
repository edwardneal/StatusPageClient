using System;
using System.Collections.Generic;
using System.Text;

namespace StatusPageClient.Models
{
    /// <summary>
    /// Provides a machine-readable <see cref="Indicator"/> and a human-readable <see cref="Description"/> of a status.
    /// </summary>
    public class StatusDescription : IEquatable<StatusDescription>
    {
        /// <summary>
        /// The machine-readable status.
        /// </summary>
        public string Indicator { get; private set; }

        /// <summary>
        /// The human-readable description of the status.
        /// </summary>
        public string Description { get; private set; }

        internal StatusDescription(string indicator, string description)
        {
            Indicator = indicator;
            Description = description;
        }

        /// <summary>
        /// Returns true if the other <see cref="StatusDescription"/> has the same <see cref="Indicator"/> as this one.
        /// </summary>
        /// <param name="other">The other <see cref="StatusDescription"/> to compare.</param>
        /// <returns>True if the two objects match, false otherwise</returns>
        public bool Equals(StatusDescription other)
        {
            return other != null && string.Equals(Indicator, other.Indicator, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Indicator + (string.IsNullOrWhiteSpace(Description) ? string.Empty : $" ({Description})");
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as StatusDescription);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Indicator.GetHashCode();
        }
    }
}
