using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace students_api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    

    public class StudentsController : ControllerBase
    {
            private readonly StudentRepo _context;
            
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }

// POST: Students

        [HttpPost]
        public async Task<ActionResult<Student>> PostTodoItem(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(Student),
             new { 
                nr_matricol = student.numar_matricol ,
                num = student.nume,
                prenum= student.prenume,
                fac=student.facultate,
                specializ=student.specializare,
                medie=student.medie_admitere }, student);
        }
       
       
    }
}