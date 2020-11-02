using Microsoft.WindowsAzure.Storage.Table;
namespace Models
{
    public class Metrics : TableEntity
    {
        public Metrics(string university, string timestamp)
        {
            this.PartitionKey = university;
            this.RowKey = timestamp;
        }
        public Metrics(){ }
        public int Count {get; set;}
    }
}