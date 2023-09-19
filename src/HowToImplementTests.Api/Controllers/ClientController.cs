using HowToImplementTests.Api.Models;
using HowToImplementTests.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HowToImplementTests.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;

        public ClientController(IClientRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
            return Ok(await _repository.GetAllClientsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _repository.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateClientAsync(client);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            await _repository.CreateClientAsync(client);
            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _repository.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            await _repository.DeleteClientAsync(client);

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _repository.ClientExists(id);
        }
    }
}
