using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StatusPageClient
{
    /// <summary>
    /// Root class which enables the Statuspage.io API to be queried.
    /// </summary>
    /// <remarks>
    /// The base URL is composed as "https://<see cref="PageId"/>.statuspage.io/api/{version}/"
    /// </remarks>
    public sealed class StatusPageClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// If true, the most recent maintenance events will be retrieved from maintenance-events.json and parsed.
        /// </summary>
        public bool RetrieveAllMaintenanceEvents { get; set; }

        /// <summary>
        /// If true, the most recent incidents will be retrieved from incidents.json and parsed.
        /// </summary>
        public bool RetrieveAllIncidents { get; set; }

        /// <summary>
        /// The ID of the page to retrieve, set through the constructor.
        /// </summary>
        public string PageId { get; private set; }

        public Models.StatusPage StatusPage { get; private set; }

        /// <summary>
        /// Creates an instance of the <see cref="StatusPageClient"/> class.
        /// </summary>
        /// <param name="pageId">The ID of the page to retrieve.</param>
        public StatusPageClient(string pageId)
        {
            if (string.IsNullOrWhiteSpace(pageId))
                throw new ArgumentNullException(nameof(pageId));

            PageId = pageId;

            _httpClient = new HttpClient() { BaseAddress = new Uri($"https://{PageId}.statuspage.io/api/") };
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgent);
        }

        /// <summary>
        /// Refresh data from the Statuspage.io API.
        /// </summary>
        public async Task RefreshAsync()
        {
            var taskWaitList = new List<Task> { getApiResponseAsync<ResponseObjects.SummaryResponse>("summary.json") };

            if (RetrieveAllMaintenanceEvents)
                taskWaitList.Add(getApiResponseAsync<ResponseObjects.ScheduledMaintenanceResponse>("scheduled-maintenances.json"));
            if (RetrieveAllIncidents)
                taskWaitList.Add(getApiResponseAsync<ResponseObjects.IncidentsResponse>("incidents.json"));

            await Task.WhenAll(taskWaitList);

            var deserialisedSummaryResponse = taskWaitList.OfType<Task<ResponseObjects.SummaryResponse>>().FirstOrDefault().Result;
            var deserialisedScheduledMaintenanceResponse = taskWaitList.OfType<Task<ResponseObjects.ScheduledMaintenanceResponse>>().FirstOrDefault()?.Result;
            var deserialisedIncidentsResponse = taskWaitList.OfType<Task<ResponseObjects.IncidentsResponse>>().FirstOrDefault()?.Result;

            StatusPage = new Models.StatusPage(deserialisedSummaryResponse, deserialisedScheduledMaintenanceResponse, deserialisedIncidentsResponse);
        }

        private async Task<T> getApiResponseAsync<T>(string relativePath)
        {
            var httpResponse = await _httpClient.GetAsync($"{Constants.ApiVersion}/{relativePath}");

            httpResponse.EnsureSuccessStatusCode();

            var responseText = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(responseText);
        }

        /// <summary>
        /// Disposes of the managed resources used by the <see cref="StatusPageClient"/>.
        /// </summary>
        public void Dispose()
        {
            if (_httpClient != null)
                _httpClient.Dispose();
        }
    }
}
