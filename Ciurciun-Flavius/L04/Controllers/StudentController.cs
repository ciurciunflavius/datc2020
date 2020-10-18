using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace L04.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        private IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentEntity>> Get()
        {
            return await _studentRepository.GetAllStudents();
        }

        [HttpPost]
        public async Task Post([FromBody] StudentEntity student)
        {
            try
            {
                await _studentRepository.CreateStudent(student);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        [HttpPut]
        public async Task Put([FromBody] StudentEntity student)
        {
            try
            {
                await _studentRepository.UpdateStudent(student);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        [HttpDelete]
        public async Task Delete([FromBody] StudentEntity student)
        {
            try
            {
                await _studentRepository.DeleteStudent(student);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}
