using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Models;

namespace DATC.Students 
{
    public static class DATCL06
    {
        [FunctionName("DATCL06")]
        [return: Table("students")]
        public static StudentEntity Run([QueueTrigger("students-queue", Connection = "storagedatc")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            var student = JsonConvert.DeserializeObject<StudentEntity>(myQueueItem);

            return student;
            }
    }
}
