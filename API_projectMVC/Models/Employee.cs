using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API_projectMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Employee Name")]
        public string name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int Deptid { get; set; }
    }
}
