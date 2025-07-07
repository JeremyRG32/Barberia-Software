using Barberia.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Barberia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private static List<Cliente> clientes = new List<Cliente>();
        private static int nextId = 1;

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult<Cliente> Create([FromBody] Cliente cliente)
        {
            cliente.Id = nextId++;
            clientes.Add(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Cliente clienteActualizado)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
                return NotFound();

            cliente.Nombre = clienteActualizado.Nombre;
            cliente.Email = clienteActualizado.Email;
            cliente.Telefono = clienteActualizado.Telefono;
            cliente.Estado = clienteActualizado.Estado;
            cliente.Direccion = clienteActualizado.Direccion;
            cliente.LocalesFavoritos = clienteActualizado.LocalesFavoritos;

            return NoContent();
        }
    }
}
