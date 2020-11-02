using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class StudentEntity : TableEntity
    {
        public StudentEntity(string faculty, string cnp)
        {
            this.PartitionKey = faculty;
            this.RowKey = cnp;
        }

        public StudentEntity(){ }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int YearOfStudy { get; set; }
        public string Faculty { get; set; }
    }
}