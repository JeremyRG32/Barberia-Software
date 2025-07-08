using Barberia.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Barberia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private static List<Usuario> usuarios = new List<Usuario>();
        private static int nextId = 1;

        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetById(int id)
        {
            var cliente = usuarios.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult<Usuario> Create([FromBody] Usuario usuario)
        {
            usuario.Id = nextId++;
            usuarios.Add(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Usuario usuarioActualizado)
        {
            var usuario = usuarios.FirstOrDefault(c => c.Id == id);
            if (usuario == null)
                return NotFound();

            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.Email = usuarioActualizado.Email;
            usuario.Telefono = usuarioActualizado.Telefono;
            usuario.Estado = usuarioActualizado.Estado;
            usuario.Direccion = usuarioActualizado.Direccion;
            usuario.LocalesFavoritos = usuarioActualizado.LocalesFavoritos;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var cliente = usuarios.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
                return NotFound();

            usuarios.Remove(cliente);
            return NoContent();
        }
    }
}
