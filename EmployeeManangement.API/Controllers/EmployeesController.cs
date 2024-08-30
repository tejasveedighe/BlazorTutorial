using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;
using EmployeeManangement.API.Data;
using EmployeeManangement.API.Models;
using Microsoft.CodeAnalysis;

namespace EmployeeManangement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetTejasvee_Blazor_Employees()
        {
            return await _context.Tejasvee_Blazor_Employees.Include(e => e.Department).ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Tejasvee_Blazor_Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, PostEmployeeDTO model)
        {
            var employee = await _context.Tejasvee_Blazor_Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Employee not found");
            }

            employee.Email = model.Email;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.PhotoPath = model.PhotoPath;
            employee.DateOfBirth = model.DateOfBirth;

            if (employee.Department.DepartmentId != model.DeptId)
            {
                var dept = await _context.Tejasvee_Blazor_Department.FindAsync(model.DeptId);
                if (dept == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Department Not found");
                }

                employee.Department = dept;
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Employee");
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(PostEmployeeDTO model)
        {
            var dept = await _context.Tejasvee_Blazor_Department.FindAsync(model.DeptId);
            if (dept == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Department Not found");
            }
            Employee employee = new Employee()
            {
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhotoPath = model.PhotoPath,
                Department = dept
            };
            _context.Tejasvee_Blazor_Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Tejasvee_Blazor_Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Tejasvee_Blazor_Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Tejasvee_Blazor_Employees.Any(e => e.EmployeeId == id);
        }
    }
}
