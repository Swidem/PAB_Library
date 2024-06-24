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
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        //private readonly IValidator<CreateClientDto> _validator;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> GetClients()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich klientów");
            var result = _clientService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich klientów");
            return Ok(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ClientDto> GetClient(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                var client = _clientService.GetById(id);
                return Ok(client);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateClient([FromBody] CreateClientDto createClientDto)
        {
            //var validationResult = _validator.Validate(createClientDto);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}

            var clientId = _clientService.Create(createClientDto);
            var actionName = nameof(GetClient);
            var routeValues = new { id = clientId };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] UpdateClientDto updateClientDto)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                _clientService.Update(updateClientDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                _clientService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}