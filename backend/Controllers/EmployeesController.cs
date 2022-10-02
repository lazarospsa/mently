using mently.Data;
using mently.Models;
using Microsoft.AspNetCore.Mvc;

namespace mently.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly mentlyDbContext _mentlyDbContext;
        public EmployeesController(mentlyDbContext mentlyDbContext)
        {
            _mentlyDbContext = mentlyDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = _mentlyDbContext.Employees.ToList();

            return Ok(employees);
        }


        
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _mentlyDbContext.Employees.AddAsync(employeeRequest);
            await _mentlyDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }
    }
}