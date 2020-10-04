using Microsoft.EntityFrameworkCore;

namespace students_api
{
    public class StudentRepo : DbContext
    {
        public StudentRepo(DbContextOptions<StudentRepo>options):base(options)
        {}
        public DbSet<Student>Students{get;set;}
    }
}