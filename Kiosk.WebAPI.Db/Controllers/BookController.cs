using FluentValidation;
using Kiosk.WebAPI.Db.Dto;
using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Db.Persistance;
using Kiosk.WebAPI.Db.Services;
using Kiosk.WebAPI.Persistance;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kiosk.WebAPI.Db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooks()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich książek");
            var result = _bookService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich książek");
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BookDto> GetBook(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                var book = _bookService.GetById(id);
                return Ok(book);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateBook([FromBody] CreateBookDto createBookDto)
        {
            //var validationResult = _validator.Validate(createBookDto);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}

            var bookId = _bookService.Create(createBookDto);
            var actionName = nameof(GetBook);
            var routeValues = new { id = bookId };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                _bookService.Update(updateBookDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                _bookService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}