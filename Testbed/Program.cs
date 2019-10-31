using System;

namespace Testbed
{
    class Program
    {
        static void Main(string[] args)
        {
            var pageId = "3tgl2vf85cht";
            var spc = new StatusPageClient.StatusPageClient(pageId) { RetrieveAllIncidents = false, RetrieveAllMaintenanceEvents = true };

            spc.RefreshAsync().Wait();

            Console.WriteLine($"{spc.StatusPage}: {spc.StatusPage.OverallStatus}");
            Console.WriteLine("\tComponents:");
            foreach(var comp in spc.StatusPage.Components)
            {
                Console.WriteLine($"\t\t{comp}: {comp.Status}");
            }
            Console.WriteLine("\tIncidents:");
            foreach(var inc in spc.StatusPage.Incidents)
            {
                Console.WriteLine($"\t\t{inc}: {inc.Status}");
                Console.WriteLine($"\t\tUpdates");
                foreach (var incUpd in inc.Updates)
                {
                    Console.WriteLine($"\t\t\t{incUpd}");
                }
            }
            Console.WriteLine("\tScheduled Maintenance Events:");
            foreach (var schedMaint in spc.StatusPage.ScheduledMaintenances)
            {
                Console.WriteLine($"\t\t{schedMaint}: {schedMaint.Status}");
                Console.WriteLine($"\t\tUpdates");
                foreach (var schedMaintUpd in schedMaint.Updates)
                {
                    Console.WriteLine($"\t\t\t{schedMaintUpd}");
                }
            }
            Console.ReadLine();
        }
    }
}
