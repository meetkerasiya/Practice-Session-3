using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.API.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty; 
    }
}
