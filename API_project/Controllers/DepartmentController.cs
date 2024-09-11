using API_project.Data;
using API_project.Models;
using Microsoft.AspNetCore.Mvc;
using DTO=API_project.Models.DTO;

namespace API_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly OfficeContext _officeContext;
        public DepartmentController(OfficeContext officeContext)
        {
            _officeContext = officeContext;
        }
        //GET
        [HttpGet]
        public IActionResult Department() 
        { 
            var allDepartment=_officeContext.Departments.OrderBy(x=>x.Id).ToList();
            if (allDepartment == null)
            {
                return NotFound("Department is null");
            }
            return Ok(allDepartment);
        }
        //Search-by name
        [HttpGet("by-name/{Ename}")]
        public IActionResult getDepartmentByName(String Ename)
        {
            var department = _officeContext.Departments.FirstOrDefault(x => x.Name==Ename);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        //POST
        [HttpPost]
        public IActionResult postDept([FromBody] DTO.Department department)
        {
            if (department == null)
            {
                return BadRequest("Department is null");
            }
            var new_department = new Department
            {
                Id = department.Id,
                Name = department.Name
            };
            _officeContext.Add(new_department);
            _officeContext.SaveChanges();
            return Ok(new_department);
        }
        //UPDATE
        [HttpPut("{deptName}")]
        public IActionResult UpdateDept(string deptName, [FromBody] DTO.Departments department)
        {
            var updateDept=_officeContext.Departments.FirstOrDefault(x=>x.Name == deptName);
            if (updateDept == null)
            {
                return BadRequest();
            }

            if (updateDept.Name != null)
            {
                updateDept.Name = department.Name;
            }

            _officeContext.SaveChanges();
            return Ok(updateDept);
        }

        //DELETE
        [HttpDelete("{deptName}")]
        public IActionResult DeleteDept(string deptName) 
        { 
            var dept=_officeContext.Departments.FirstOrDefault(x=>x.Name == deptName);
            _officeContext.Departments.Remove(dept);
            _officeContext.SaveChanges();
            return Ok(dept);
        }
    }
}
