using Barberia.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Barberia.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClientesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        //private static List<Cliente> _clientes = new List<Cliente>
        //{
        //    new Cliente
        //    {
        //        Id = 1,
        //        Nombre = "Juan Pérez",
        //        Email = "juan@perez.com",
        //        Telefono = "809-123-4567",
        //        Direccion = "Calle Altagracia",
        //        Estado = Cliente.EstadoCliente.EnCola
        //    },
        //    new Cliente
        //    {
        //        Id = 2,
        //        Nombre = "Ana Gómez",
        //        Email = "ana@gomez.com",
        //        Telefono = "829-987-6543",
        //        Direccion = "Av. Siempre Viva 742",
        //        Estado = Cliente.EstadoCliente.Atendido
        //    }
        //};

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7179/api/clientes");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);
                return View(clientes);
            }
            return View(new List<Cliente>());
        }

        // GET: Clientes/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7179/api/clientes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(json);
                return View(cliente);
            }

            return NotFound();
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7179/api/clientes", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7179/api/clientes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(json);
                return View(cliente);
            }

            return NotFound();
        }

        // POST: Clientes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"https://localhost:7179/api/clientes/{id}", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        // GET: Clientes/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7179/api/clientes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(json);
                return View(cliente);
            }

            return NotFound();
        }

        // POST: Clientes/DeleteConfirmed/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7179/api/clientes/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
