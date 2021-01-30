using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Common
{
    public class QuartzConfiguration
    {
        public static NameValueCollection RemoteConfig()
        {
            NameValueCollection configuration = new NameValueCollection
            {
                 { "quartz.scheduler.instanceName", "RemoteServer" },
                 { "quartz.scheduler.instanceId", "RemoteServer" },
                 { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
                 { "quartz.jobStore.useProperties", "true" },
                 { "quartz.jobStore.dataSource", "default" },
                 { "quartz.jobStore.tablePrefix", "QRTZ_" },
                 { "quartz.dataSource.default.connectionString", "Server=(servername);Database=(datbasename);Trusted_Connection=true;" },
                 { "quartz.dataSource.default.provider", "SqlServer" },
                 { "quartz.threadPool.threadCount", "1" },
                 { "quartz.serializer.type", "binary" },
            };

            return configuration;
        }

        public static NameValueCollection LocalConfig(bool useSqlServer)
        {
            if (useSqlServer)
            {
                return new NameValueCollection
                {
                     { "quartz.scheduler.instanceName", "LocalServer" },
                     { "quartz.scheduler.instanceId", "LocalServer" },
                     { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
                     { "quartz.jobStore.useProperties", "true" },
                     { "quartz.jobStore.dataSource", "default" },
                     { "quartz.jobStore.tablePrefix", "QRTZ_" },
                     { "quartz.dataSource.default.connectionString", "Server=(servername);Database=(datbasename);Trusted_Connection=true;" },
                     { "quartz.dataSource.default.provider", "SqlServer" },
                     { "quartz.threadPool.threadCount", "1" },
                     { "quartz.serializer.type", "binary" },
                };
            }
            else
            {
                return new NameValueCollection
                {
                    { "quartz.scheduler.instanceName", "LocalServer" },
                     { "quartz.scheduler.instanceId", "LocalServer" },
                     { "quartz.threadPool.threadCount", "1" },
                     { "quartz.serializer.type", "binary" },
                };
            }
        }
    }
}
