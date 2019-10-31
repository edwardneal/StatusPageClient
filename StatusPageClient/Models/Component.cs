using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatusPageClient.Models
{
    /// <summary>
    /// A single component of the <see cref="StatusPage"/> which can have its own dependencies and status.
    /// </summary>
    public class Component : IEquatable<Component>
    {
        private IEnumerable<string> _rawSubcomponents;

        /// <summary>
        /// The unique identifier for this <see cref="Component"/>.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The friendly name of this <see cref="Component"/>.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A human-readable description of this <see cref="Component"/>.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The order that this <see cref="Component"/> should appear in.
        /// </summary>
        public int DisplayPosition { get; private set; }

        /// <summary>
        /// Current status of this <see cref="Component"/>
        /// </summary>
        /// <remarks>
        /// Only the <see cref="StatusDescription.Indicator"/> property will be populated.
        /// </remarks>
        public StatusDescription Status { get; private set; }

        /// <summary>
        /// All child components of this <see cref="Component"/>.
        /// </summary>
        public IEnumerable<Component> Subcomponents { get; private set; }

        internal Component(ResponseObjects.Component respComponent)
        {
            _rawSubcomponents = respComponent.SubcomponentIds ?? Enumerable.Empty<string>();

            Id = respComponent.Id;
            Name = respComponent.Name;
            Description = respComponent.Description;
            DisplayPosition = respComponent.Position;
            Status = new StatusDescription(respComponent.Status, null);
        }

        internal void ProcessSubcomponents(IEnumerable<Component> processedComponents)
        {
            var componentDictionary = processedComponents.ToDictionary(pc => pc.Id);

            Subcomponents = (from id in _rawSubcomponents
                             where componentDictionary.ContainsKey(id)
                             select componentDictionary[id]).ToArray();
        }

        /// <summary>
        /// Returns true if the other <see cref="Component"/> instance has the same <see cref="Id"/> as this one.
        /// </summary>
        /// <param name="other">The other <see cref="Component"/> to compare.</param>
        /// <returns>True if the two objects match, false otherwise</returns>
        public bool Equals(Component other)
        {
            return other != null && string.Equals(Id, other.Id, StringComparison.InvariantCultureIgnoreCase);
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
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Component);
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
