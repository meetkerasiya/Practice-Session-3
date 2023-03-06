namespace Students.API.Models.Repository
{
    public class StudentRepository : IDataRepository<Student>
    {
        private readonly StudentContext _context;

        public StudentRepository(StudentContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student Get(int id)
        {
            return _context.Students
                .FirstOrDefault(s => s.StudentID == id);
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student, Student entity)
        {
            student.FirstName = entity.FirstName;
            student.LastName = entity.LastName;
            student.City = entity.City;

            _context.SaveChanges();
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
