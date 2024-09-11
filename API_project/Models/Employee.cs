using System.ComponentModel.DataAnnotations.Schema;

namespace API_project.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string PhoneNumber { get; set; }
        public int Deptid { get; set; }

        [ForeignKey("Deptid")]
        public Department Department { get; set; }
    }
}
