using FluentValidation;
using Kiosk.WebAPI.Db.Dto;
using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Db.Persistance;
using Kiosk.WebAPI.Db.Services;
using Kiosk.WebAPI.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Kiosk.WebAPI.Db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        //private readonly IValidator<CreateEmployeeDto> _validator;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployees()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich pracowników");
            var result = _employeeService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich pracowników");
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EmployeeDto> GetEmployee(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                var employee = _employeeService.GetById(id);
                return Ok(employee);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            //var validationResult = _validator.Validate(createEmployeeDto);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}

            var employeeId = _employeeService.Create(createEmployeeDto);
            var actionName = nameof(GetEmployee);
            var routeValues = new { id = employeeId };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                _employeeService.Update(updateEmployeeDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                _employeeService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}