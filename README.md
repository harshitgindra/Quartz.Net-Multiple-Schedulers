# Quartz.Net-Multiple-Schedulers
This project demonstrates the use of multiple job listeners for Quartz job scheduler.

# Introduction
This project is related to using multiple job listeners for one application that are connected to same SQL datastore. The posible use for this approach is when we need to off-load certain jobs to be executed on a different server. Here in this example, there are two listeners(console applications) and a Web application to put jobs on queue. After jobs are placed on the queue, depending on the configuration, jobs will be picked up by the responsible scheduler and executed.

# Getting Started
There are two aspects to Quartz.
1. Configure quartz to only be able to configure jobs 
2. Configure quartz to be able to configure and execute jobs

## Configure Quartz to only queue jobs
This following method will initialize/create an instance of Quartz Scheduler from the factory. With that instance, one can create a job, associate job data and attach a trigger to the job to specify when it should be executed. 
```
        private async Task CreateJob(NameValueCollection configuration)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory(configuration);
            IScheduler sched = await factory.GetScheduler();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity($"HelloJob-{DateTime.Now}", "Group1")
                .UsingJobData("DateTime", DateTime.Now.ToString())
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity($"Trigger at {DateTime.Now}", "group1")
              .StartNow()
              .Build();

            await sched.ScheduleJob(job, trigger);
        }
```
We can configure the Quartz settings in multiple ways like `app.config`, `app_start` or using `NameValueCollection`. In this example, I've used `NameValueCollection`.
`StdSchedulerFactory factory = new StdSchedulerFactory(configuration);`

This following line queues the job in the quartz service
`await sched.ScheduleJob(job, trigger);`

## Configure Quartz to queue and execute jobs

```
        private async Task CreateJob(NameValueCollection configuration)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory(configuration);
            IScheduler sched = await factory.GetScheduler();
            
            if (!sched.IsStarted)
            {
                await sched.Start();
            }

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity($"HelloJob-{DateTime.Now}", "Group1")
                .UsingJobData("DateTime", DateTime.Now.ToString())
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity($"Trigger at {DateTime.Now}", "group1")
              .StartNow()
              .Build();

            await sched.ScheduleJob(job, trigger);
        }
```

The following line actually controls if the configured quartz service should be allowed to execute jobs or not. By adding the following line, quartz will now monitor the job queue and execute jobs if there are any.
`await sched.Start();`

## Using multiple schedulers
Quartz is pretty configurable and allows us a feature use multiple quartz schedulers monitoring the same job queue. One can also use one instance to only queue jobs and other instances to only execute the jobs. This is controlled by `Instance ID` and `Instance Name`. Depending upon what instance details are used to configure the job, that instance will be able to execute the job.

```
      { "quartz.scheduler.instanceName", "RemoteServer" },
      { "quartz.scheduler.instanceId", "RemoteServer" },
```

## Jobs
Per Quartz documentation, the job that needs to be executed needs to implement `Quartz.IJob` interface.


```

public class HelloJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string dateTime = dataMap.GetString("DateTime");

            await Console.Out.WriteLineAsync("Hello Job started");

            for (int i = 0; i < 5; i++)
            {
                await Console.Out.WriteLineAsync($"Running step {i} at {dateTime}");

                Thread.Sleep(2000);
            }

            await Console.Out.WriteLineAsync("Hello Job completing");

        }
    }
```

# Note
Make sure that you've configured your database with Quartz tables and replace the connectionstring in the project. 

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.


<!-- CONTACT -->
## Contact

Your Name - [@harshitgindra](https://twitter.com/harshitgindra) - harshitgindra@gmail.com

Project Link: [https://github.com/harshitgindra/Quartz.Net-Multiple-Schedulers](https://github.com/harshitgindra/Quartz.Net-Multiple-Schedulers)

<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [Img Shields](https://shields.io)
* [Choose an Open Source License](https://choosealicense.com)
* [Quartz.Net](https://www.quartz-scheduler.net)
