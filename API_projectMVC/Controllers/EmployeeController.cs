using API_projectMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace API_projectMVC.Controllers
{
    public class EmployeeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7183/api/Employee");
        private readonly HttpClient _httpClient;
        public async Task<IActionResult> Index()
        {
            var employeeList = await GetEmployee(); 
            return View("GetEmployee", employeeList);
        }
        public EmployeeController()
        {
            _httpClient=new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }





        //Get
        [HttpGet]
        public async Task<List<Employee>> GetEmployee()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7183/api/Employee/getEmployees/get_all_e");

            List<Employee> employeeList = new List<Employee>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employeeList= JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return employeeList ?? new List<Employee>(); 
        }



        //create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmployeePost(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var EmployeeWithStringDate = new
                {
                    employee.name,
                    employee.PhoneNumber,
                    employee.Deptid
                };

                var json = JsonConvert.SerializeObject(EmployeeWithStringDate);

                System.Diagnostics.Debug.WriteLine("JSON Payload: " + json);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7183/api/Employee/postEmployees/create_e", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Server Error" + response.ReasonPhrase);
                }
            }
            return View(employee);
        }




        //Update
        [HttpGet]
        public async Task<IActionResult> Edit(string Ename)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7183/api/Employee/getEmployeeByName/by-name/{Ename}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var employee = JsonConvert.DeserializeObject<Employee>(data);
                return View(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(employee);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7183/api/Employee/updateEmployee/update_e/{employee.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error: " + response.ReasonPhrase);
                }
            }
            return View(employee);
        }





        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string Ename)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7183/api/Employee/getEmployeeByName/by-name/{Ename}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var employee = JsonConvert.DeserializeObject<Employee>(data);
                return View(employee);
            }
            return NotFound();
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7183/api/Employee/DeleteEmployee/delete_e/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
