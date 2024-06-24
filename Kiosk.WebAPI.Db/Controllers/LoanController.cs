using FluentValidation;
using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Db.Persistance;
using Kiosk.WebAPI.Db.Services;
using Kiosk.WebAPI.Dto;
using Kiosk.WebAPI.Models;
using Kiosk.WebAPI.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Kiosk.WebAPI.Db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        //private readonly IValidator<CreateLoanDto> _validator;
        private readonly ILogger<LoanController> _logger;

        public LoanController(ILoanService loanService, ILogger<LoanController> logger)
        {
            _loanService = loanService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LoanDto>> GetLoans()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich wypożyczeń");
            var result = _loanService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich wypożyczeń");
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LoanDto> GetLoan(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                var loan = _loanService.GetById(id);
                return Ok(loan);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateLoan([FromBody] CreateLoanDto createLoanDto)
        {
            //var validationResult = _validator.Validate(createLoanDto);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}

            var loanId = _loanService.Create(createLoanDto);
            var actionName = nameof(GetLoan);
            var routeValues = new { id = loanId };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLoan(int id, [FromBody] UpdateLoanDto updateLoanDto)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                _loanService.Update(updateLoanDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLoan(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                _loanService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}