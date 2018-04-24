using Quartz;
using Quartz.Impl;
using Scheduler.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Local
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Task.Run(async () =>
                {
                    StdSchedulerFactory factory = new StdSchedulerFactory(QuartzConfiguration.LocalConfig());

                    // get a scheduler
                    IScheduler sched = await factory.GetScheduler();

                    if (!sched.IsStarted)
                        await sched.Start();

                    Console.WriteLine("Local Server has been started..");

                    while (true)
                    {
                        Thread.Sleep(60 * 60000);
                    }
                }).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
