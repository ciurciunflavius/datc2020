using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Models;

namespace L04
{
    public class StudentRepository : IStudentRepository
    {
        private string _connectionString;
        private CloudTableClient _tableClient;
        private CloudTable _studentsTable;
        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("AzureStorageAccountConnectionString");
           Task.Run(async () => { await InitializeTable();}).GetAwaiter().GetResult();
        }
        public async Task DeleteStudent(string partitionKey, string rowKey)
        {
            TableOperation retrieve = TableOperation.Retrieve<StudentEntity>(partitionKey, rowKey);
            TableResult result = await _studentsTable.ExecuteAsync(retrieve);

            StudentEntity deleteStudent = (StudentEntity)result.Result;
            TableOperation delete = TableOperation.Delete(deleteStudent);
            await _studentsTable.ExecuteAsync(delete);
        }
        public async Task<List<StudentEntity>> GetAllStudents()
        {
            List<StudentEntity> students = new List<StudentEntity>();
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>();
            TableContinuationToken token = null;
            do{
                TableQuerySegment<StudentEntity> resultSegment = await _studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                students.AddRange(resultSegment.Results);
            }while(token != null);
            return students;
        }
        public async Task InsertNewStudent(StudentEntity student)
        {
            StudentEntity newStudent = new StudentEntity(student.Faculty, student.CNP);
            newStudent.FirstName = student.FirstName;
            newStudent.LastName = student.LastName;
            newStudent.CNP = student.CNP;
            newStudent.Email = student.Email;
            newStudent.PhoneNumber = student.PhoneNumber;
            newStudent.YearOfStudy = student.YearOfStudy;
            newStudent.Faculty = student.Faculty;
            string jsonStudent = JsonConvert.SerializeObject(newStudent);
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonStudent);
            string base64String = System.Convert.ToBase64String(plainTextBytes);
            QueueClient queueClient = new QueueClient(
                _connectionString,
                "students-queue"
            );
            queueClient.CreateIfNotExistsAsync().GetAwaiter().GetResult();
            await queueClient.SendMessageAsync(base64String);
        }
        public async Task UpdateStudent(string partitionKey, string rowKey, StudentEntity student)
        {
            student.PartitionKey = partitionKey;
            student.RowKey = rowKey;
            student.ETag = "*";
            TableOperation update = TableOperation.Replace(student);
            await _studentsTable.ExecuteAsync(update);
        }
        private async Task InitializeTable()
        {
                var account = CloudStorageAccount.Parse(_connectionString);
                _tableClient = account.CreateCloudTableClient();
                _studentsTable = _tableClient.GetTableReference("students");
                await _studentsTable.CreateIfNotExistsAsync();
        }
    }
}