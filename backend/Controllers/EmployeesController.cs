using mently.Data;
using mently.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var employees = await _mentlyDbContext.Employees.ToListAsync();

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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _mentlyDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            Employee employee = await GetEmployees().FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _mentlyDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        private DbSet<Employee> GetEmployees()
        {
            return _mentlyDbContext.Employees;
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            Employee employee = await GetEmployees().FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            // Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Employee> entityEntry = _mentlyDbContext.Employees.Remove(employee);
            GetEmployees().Remove(employee);
            return Ok();
        }
    }
}