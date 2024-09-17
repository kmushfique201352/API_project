using API_project.Data;
using API_project.Models;
using API_project.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using DTO=API_project.Models.DTO;

namespace API_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //GET
        [HttpGet]
        public IActionResult Department() 
        { 
            var allDepartment=_unitOfWork.Departments.GetAll().OrderBy(x=>x.Id).ToList();
            if (allDepartment == null)
            {
                return NotFound("Department is null");
            }
            return Ok(allDepartment);
        }
        //search-by id
        [HttpGet("by-id/{id}")]
        public IActionResult getDepartmentByName(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        //Search-by name
        [HttpGet("by-name/{Ename}")]
        public IActionResult getDepartmentByName(String Ename)
        {
            var department = _unitOfWork.Departments.GetAll().FirstOrDefault(x => x.Name==Ename);
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
            _unitOfWork.Departments.Add(new_department);
            _unitOfWork.Save();
            return Ok(new_department);
        }
        //UPDATE
        [HttpPut("{id}")]
        public IActionResult UpdateDept(int id, [FromBody] DTO.Departments department)
        {
            var updateDept = _unitOfWork.Departments.GetById(id);
            if (updateDept == null)
            {
                return BadRequest();
            }

            if (updateDept.Name != null)
            {
                updateDept.Name = department.Name;
            }
            updateDept.Name = updateDept.Name?? updateDept.Name;

            _unitOfWork.Save();
            return Ok(updateDept);
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteDept(int id) 
        {
            var dept = _unitOfWork.Departments.GetById(id);
            _unitOfWork.Departments.Delete(id);
            _unitOfWork.Save();
            return Ok(dept);
        }
    }
}
