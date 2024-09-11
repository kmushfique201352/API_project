using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API_projectMVC.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Department Name")]
        public string Name { get; set; }
    }
}
