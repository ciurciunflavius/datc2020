using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

public interface IStudentRepository
{
    Task<List<StudentEntity>> GetAllStudents();

    Task InsertNewStudent(StudentEntity student);

    Task UpdateStudent(string partitionKey, string rowKey, StudentEntity student);

    Task DeleteStudent(string partitionKey, string rowKey);
}