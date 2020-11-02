using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class Student : TableEntity
    {
        public Student(string university, string cnp)
        {
            this.PartitionKey = university;
            this.RowKey = cnp;
        }

        public Student(){ }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int YearOfStudy { get; set; }
        public string Faculty { get; set; }
    }
}