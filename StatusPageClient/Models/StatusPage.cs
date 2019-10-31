using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StatusPageClient.Models
{
    /// <summary>
    /// The root of the Statuspage.io object model. Provides base details about the subscription,
    /// and enables a deeper view of the related components and incidents.
    /// </summary>
    public class StatusPage : IEquatable<StatusPage>
    {
        /// <summary>
        /// The unique identifier for this status page.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The friendly name of this status page.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The friendly URL of this status page.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The overall status of this <see cref="StatusPage"/>, rolled up from its <see cref="Components"/>.
        /// </summary>
        public StatusDescription OverallStatus { get; private set; }

        /// <summary>
        /// All components associated with this status page.
        /// </summary>
        public IEnumerable<Component> Components { get; private set; }

        /// <summary>
        /// All incidents associated with this status page.
        /// </summary>
        public IEnumerable<Incident> Incidents { get; private set; }

        /// <summary>
        /// All scheduled maintenance events associated with this status page.
        /// </summary>
        public IEnumerable<ScheduledMaintenance> ScheduledMaintenances { get; private set; }

        internal StatusPage(ResponseObjects.SummaryResponse summaryResponse, ResponseObjects.ScheduledMaintenanceResponse scheduledMaintenanceResponse, ResponseObjects.IncidentsResponse incidentsResponse)
        {
            if (summaryResponse == null)
                throw new ArgumentNullException(nameof(summaryResponse));

            Id = summaryResponse.Page.Id;
            Name = summaryResponse.Page.Name;
            Url = summaryResponse.Page.Url;

            OverallStatus = new StatusDescription(summaryResponse.Status.Indicator, summaryResponse.Status.Description);

            Components = summaryResponse.Components.Select(c => new Component(c)).ToArray();
            foreach(var c in Components)
            {
                c.ProcessSubcomponents(Components);
            }

            Incidents = summaryResponse.Incidents.Select(i => new Incident(i, Components)).ToArray();

            if (incidentsResponse != null && incidentsResponse.Incidents != null)
            {
                var priorIncidents = incidentsResponse.Incidents.Select(i => new Incident(i, Components)).ToArray();

                Incidents = Incidents.Concat(priorIncidents.Except(Incidents)).ToArray();
            }

            // The summary response will have currently active scheduled maintenance records in, but if we've got the extra data then we should use it.
            ScheduledMaintenances = summaryResponse.ScheduledMaintenances.Select(schedMaint => new ScheduledMaintenance(schedMaint, Components)).ToArray();

            if (scheduledMaintenanceResponse != null && scheduledMaintenanceResponse.ScheduledMaintenances != null)
            {
                var priorScheduledMaintenance = scheduledMaintenanceResponse.ScheduledMaintenances.Select(schedMaint => new ScheduledMaintenance(schedMaint, Components)).ToArray();

                ScheduledMaintenances = ScheduledMaintenances.Concat(priorScheduledMaintenance.Except(ScheduledMaintenances)).ToArray();
            }
        }

        /// <summary>
        /// Returns true if the other <see cref="StatusPage"/> instance has the same <see cref="Id"/> as this one.
        /// </summary>
        /// <param name="other">The other <see cref="StatusPage"/> to compare.</param>
        /// <returns>True if the two objects match, false otherwise</returns>
        public bool Equals(StatusPage other)
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
            return Equals(obj as StatusPage);
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
