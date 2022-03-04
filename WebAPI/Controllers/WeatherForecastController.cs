using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private ApplicationDbContext _applicationDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,ApplicationDbContext applicationDbContext )
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet]
        [Route("GetEmployee/{EmployeeID}")]
        public IEnumerable<Employee> GetEmployee(int EmployeeID)
        {
            IEnumerable<Employee> employees = null;
            try
            {
                if (EmployeeID != 0)
                {
                    employees = _applicationDbContext.Employees.Where(x => x.EmployeeId == EmployeeID).ToList();                    
                }                               
            }            
            catch (Exception ex)
            {
            }
            return employees;

        }

        [HttpGet]
        [Route("GetAllEmployee")]
        public IEnumerable<Employee> GetAllEmployee()
        {
            IEnumerable<Employee> employees = null;
            try
            {
                employees = _applicationDbContext.Employees;
            }
            catch (Exception ex)
            {
            }
            return employees;
        }

        [HttpPost]
        [Route("InsertEmployee")]
        public int InsertEmployee([FromBody] Employee value)
        {
            int result = 0;
            try
            {
                _applicationDbContext.Employees.Add(value);
                result=_applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public int UpdateEmployee([FromBody] Employee value)
        {
            int result=0;
            try
            {
                Employee employee = _applicationDbContext.Employees.FirstOrDefault(x => x.EmployeeId == value.EmployeeId);
                if(employee != null)
                {
                    employee.FirstName = value.FirstName;
                    employee.LastName = value.LastName;
                    employee.EmailID = value.EmailID;                    
                    result = _applicationDbContext.SaveChanges();
                }                                
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        [HttpDelete]
        [Route("DeleteEmployee/{EmployeeID}")]
        public ActionResult DeleteEmployee(int EmployeeID)
        {            
            try
            {
               Employee employee = _applicationDbContext.Employees.FirstOrDefault(x => x.EmployeeId == EmployeeID);
                if (employee != null)
                {
                    _applicationDbContext.Employees.Remove(employee);
                     _applicationDbContext.SaveChanges();
                    return Ok("Employee Deleted successfully!!");
                }
                else
                {
                    return NotFound("Employee Details not available!!");
                }
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }            
        }

    }
}
