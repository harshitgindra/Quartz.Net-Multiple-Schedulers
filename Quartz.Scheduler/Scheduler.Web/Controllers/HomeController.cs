using Quartz;
using Quartz.Impl;
using Scheduler.Common;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Scheduler.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Remote()
        {
            await CreateJob(QuartzConfiguration.RemoteConfig(), "remote", false);
            return View("Index");
        }

        public async Task<ActionResult> Local()
        {
            await CreateJob(QuartzConfiguration.LocalConfig(false), "local", true);
            return View("Index");
        }

        private async Task CreateJob(NameValueCollection configuration, string serverName, bool runLocally)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory(configuration);
            IScheduler sched = await factory.GetScheduler();

            if (runLocally && !sched.IsStarted)
            {
                await sched.Start();
            }

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity($"HelloJob-{DateTime.Now}", "Group1")
                .UsingJobData("ServerName", serverName)
                .UsingJobData("DateTime", DateTime.Now.ToString())
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity($"Trigger at {DateTime.Now}", "group1")
              .StartNow()
              .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}