# Quartz.Net-Multiple-Schedulers
This project demonstrates the use of multiple job listeners for Quartz job scheduler.

#Introduction
This project is related to using multiple job listeners for one application that are connected to same SQL datastore. The posible use for this approach is when we need to off-load certain jobs to be executed on a different server. Here in this example, there are two listeners(console applications) and a Web application to put jobs on queue. After jobs are placed on the queue, depending on the configuration, jobs will be picked up by the responsible scheduler and executed.


