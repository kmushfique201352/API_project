using System.ComponentModel.DataAnnotations.Schema;

namespace API_project.Models.DTO
{
    public class Employee
    {
        public string name { get; set; }
        public string PhoneNumber { get; set; }
        public int Deptid { get; set; }
    }
}
