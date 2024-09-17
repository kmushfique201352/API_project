using API_project.Data;
using API_project.Models;
using DTO=API_project.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using API_project.UnitOfWork;

namespace API_project.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        //Read
        [HttpGet("get_all_e/")]
        public IActionResult getEmployees() 
        {
            var allEmployees = _unitOfWork.Employees.GetAll().OrderBy(x => x.Id).ToList();
            if (allEmployees == null)
            {
                return NotFound();
            }
            return Ok(allEmployees);
        }
        //Search-by name
        [HttpGet("by-name/{Ename}")]
        public IActionResult getEmployeeByName(string Ename) 
        { 
            var employee= _unitOfWork.Employees.GetAll().FirstOrDefault(x=>x.name==Ename);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //Search-by phone number
        [HttpGet("by-phone/{number}")]
        public IActionResult getEmployeeByPhone(string number)
        {
            var employee = _unitOfWork.Employees.GetAll().FirstOrDefault(a => a.PhoneNumber==number);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        //Search-by dept
        [HttpGet("by-department/{DeptId}")]
        public IActionResult getEmployeeByDept(int DeptId)
        {
            var employee = _unitOfWork.Employees.GetAll().FirstOrDefault(x => x.Deptid == DeptId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        //Post
        [HttpPost("create_e/")]
        public IActionResult postEmployees([FromBody] DTO.Employee employee) 
        {
            if (employee == null) 
            {
                return BadRequest("Employee is null");
            }
            var new_employee = new Employee
            {
                name=employee.name,
                PhoneNumber=employee.PhoneNumber,
                Deptid=employee.Deptid
            };
            _unitOfWork.Employees.Add(new_employee);
            _unitOfWork.Save();
            return Ok(new_employee);
        }

        //Update
        [HttpPut("update_e/{id}")]
        public IActionResult updateEmployee(int id, [FromBody] DTO.Employee employee)
        {
            var updatedEmployee = _unitOfWork.Employees.GetById(id);
            if (updatedEmployee == null)
            {
                return BadRequest("Employee is null");
            }

            updatedEmployee.name = employee.name ?? updatedEmployee.name;
            updatedEmployee.PhoneNumber = employee.PhoneNumber ?? updatedEmployee.PhoneNumber;
            updatedEmployee.Deptid = employee.Deptid != 0 ? employee.Deptid : updatedEmployee.Deptid;

            _unitOfWork.Employees.Update(updatedEmployee);
            _unitOfWork.Save();
            return Ok(updatedEmployee);
        }


        //Delete
        [HttpDelete("delete_e/{id}")]
        public IActionResult DeleteEmployee(int id) 
        {
            var employee = _unitOfWork.Employees.GetById(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            _unitOfWork.Employees.Delete(id);
            _unitOfWork.Save();
            return Ok(employee);
        }
    }
}
