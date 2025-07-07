using Microsoft.AspNetCore.Mvc;
using Barberia.Web.Models;

namespace Barberia.Web.Controllers
{
    public class ClientesController : Controller
    {
        // ⚠️ Simulación de base de datos en memoria (debes reemplazarlo por tu lógica real)
        private static List<Cliente> _clientes = new List<Cliente>
        {
            new Cliente
            {
                Id = 1,
                Nombre = "Juan Pérez",
                Email = "juan@perez.com",
                Telefono = "809-123-4567",
                Direccion = "Calle Altagracia",
                Estado = Cliente.EstadoCliente.EnCola
            },
            new Cliente
            {
                Id = 2,
                Nombre = "Ana Gómez",
                Email = "ana@gomez.com",
                Telefono = "829-987-6543",
                Direccion = "Av. Siempre Viva 742",
                Estado = Cliente.EstadoCliente.Atendido
            }
        };

        // GET: Clientes
        public IActionResult Index()
        {
            return View(_clientes);
        }

        // GET: Clientes/Details/1
        public IActionResult Details(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Id = _clientes.Max(c => c.Id) + 1;
                _clientes.Add(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/1
        public IActionResult Edit(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: Clientes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cliente cliente)
        {
            var original = _clientes.FirstOrDefault(c => c.Id == id);
            if (original == null) return NotFound();

            if (ModelState.IsValid)
            {
                original.Nombre = cliente.Nombre;
                original.Email = cliente.Email;
                original.Telefono = cliente.Telefono;
                original.Direccion = cliente.Direccion;
                original.Estado = cliente.Estado;
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        // GET: Clientes/Delete/1
        public IActionResult Delete(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: Clientes/DeleteConfirmed/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                _clientes.Remove(cliente);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
