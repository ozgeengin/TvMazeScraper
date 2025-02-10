using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace RTL.TvMazeScraper.DataSync
{
    public class TvMazeDataSyncController
    {
        [FunctionName("TvMazeDataSync")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
