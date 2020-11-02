using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using System;
using Models;

namespace L05
{
    public class MetricsRepository
    {
        private string _connectionString;
        private CloudTable _metricsTable;
        private CloudTable _studentsTable;
        private CloudTableClient _tableClient;


        private async Task InitializeTable()
        {
            var account = CloudStorageAccount.Parse(_connectionString);
            _tableClient = account.CreateCloudTableClient();
            _metricsTable = _tableClient.GetTableReference("statistics");
            _studentsTable = _tableClient.GetTableReference("students");
            await _metricsTable.CreateIfNotExistsAsync();
            await _studentsTable.CreateIfNotExistsAsync();
        }
        public void InsertStatistic(string partKey, int count)
        {
            Metrics newMetrics = new Metrics(partKey, DateTime.UtcNow.ToString().Replace("/", "."));
            newMetrics.Count = count;
            TableOperation generalInsert = TableOperation.Insert(newMetrics);
            TableResult generalResult = _metricsTable.ExecuteAsync(generalInsert).GetAwaiter().GetResult();
        }
        public async Task GetStatistics()
        {
            List<Student> students = new List<Student>();
            TableQuery<Student> query = new TableQuery<Student>();
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<Student> resultSegment = await _studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                students.AddRange(resultSegment.Results);
            } while (token != null);
            
            //statistic general
             InsertStatistic("General", students.Count);
            
            //statistic per university
            List<Student> SortedList = new List<Student>();
            SortedList = students.OrderBy(student => student.Faculty).ToList();  
            List<List<Student>> listOfList = SortedList.GroupBy(student => student.Faculty).Select(group => group.ToList()).ToList();
            listOfList.ForEach( list => 
            {
                Console.WriteLine(list.Count + " " + list[0].Faculty);
                 InsertStatistic(list[0].Faculty, list.Count);
            };
        }

       
    }
}