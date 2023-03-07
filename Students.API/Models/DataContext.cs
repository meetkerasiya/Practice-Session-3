using Microsoft.EntityFrameworkCore;

namespace Students.API.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentID = 1,
                FirstName = "Jay",
                LastName = "Laheri",
                City = "Amreli"
            }, new Student
            {
                StudentID = 2,
                FirstName = "Krupal",
                LastName = "Vasani",
                City = "Kunkavav"
            }
            );
        }
    }
}
