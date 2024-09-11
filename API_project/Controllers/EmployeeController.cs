using API_project.Data;
using API_project.Models;
using DTO=API_project.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API_project.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly OfficeContext _officeContext;
        public EmployeeController(OfficeContext officeContext) 
        {
            _officeContext = officeContext;
        }

        //Read
        [HttpGet("get_all_e/")]
        public IActionResult getEmployees() 
        {
            var allEmployee= _officeContext.Employees.OrderBy(x => x.Id).ToList();
            if (allEmployee == null) 
            { 
                return NotFound();
            }
            return Ok(allEmployee);
        }
        //Search-by name
        [HttpGet("by-name/{Ename}")]
        public IActionResult getEmployeeByName(string Ename) 
        { 
            var employee=_officeContext.Employees.FirstOrDefault(x=>x.name==Ename);
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
            var employee = _officeContext.Employees.FirstOrDefault(a => a.PhoneNumber==number);
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
            var employee = _officeContext.Employees.FirstOrDefault(x => x.Deptid == DeptId);
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
            _officeContext.Employees.Add(new_employee);
            _officeContext.SaveChanges();
            return Ok(new_employee);
        }

        //Update
        [HttpPut("update_e/{id}")]
        public IActionResult updateEmployee(int id, [FromBody] DTO.Employee employee)
        {
            var updatedEmployee=_officeContext.Employees.FirstOrDefault(x=>x.Id==id);
            if(updatedEmployee == null)
            {
                return BadRequest("Employee is null");
            }

            if (updatedEmployee.name != null)
            {
                updatedEmployee.name = employee.name;
            }
            if (updatedEmployee.PhoneNumber != null)
            {
                updatedEmployee.PhoneNumber = employee.PhoneNumber;
            }
            if (updatedEmployee.Deptid != null)
            {
                updatedEmployee.Deptid = employee.Deptid;
            }

            _officeContext.SaveChanges();
            return Ok(updatedEmployee);
        }


        //Delete
        [HttpDelete("delete_e/{id}")]
        public IActionResult DeleteEmployee(int id) 
        { 
            var employee= _officeContext.Employees.FirstOrDefault(x=>x.Id==id);
            _officeContext.Employees.Remove(employee);
            _officeContext.SaveChanges();
            return Ok(employee);
        }
    }
}
