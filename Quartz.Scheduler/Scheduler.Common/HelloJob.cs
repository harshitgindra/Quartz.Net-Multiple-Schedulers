using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Common
{
    public class HelloJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string serverName = dataMap.GetString("ServerName");
            string dateTime = dataMap.GetString("DateTime");

            await Console.Out.WriteLineAsync("Hello Job started");

            for (int i = 0; i < 5; i++)
            {
                await Console.Out.WriteLineAsync($"Running step {i} on {serverName} server at {dateTime}");

                Thread.Sleep(2000);
            }

            await Console.Out.WriteLineAsync("Hello Job completing");

        }
    }
}
