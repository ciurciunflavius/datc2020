using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace L04.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private IStudentRepository _studentsRepository;
        public StudentsController( IStudentRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentEntity>> Get()
        {
            return await _studentsRepository.GetAllStudents();
        }

        [HttpPost]
        public async Task Post([FromBody] StudentEntity student)
        {
            await _studentsRepository.InsertNewStudent(student);    
        }

        [HttpPut("{partitionKey}/{rowKey}")]
        public async Task Update([FromRoute] string partitionKey, [FromRoute] string rowKey, [FromBody] StudentEntity student)
        {
            await _studentsRepository.UpdateStudent(partitionKey, rowKey, student);
        }

        [HttpDelete("{partitionKey}/{rowKey}")]
        public async Task Delete([FromRoute] string partitionKey, [FromRoute] string rowKey)
        {
            await _studentsRepository.DeleteStudent(partitionKey, rowKey);
        }

    }
}
