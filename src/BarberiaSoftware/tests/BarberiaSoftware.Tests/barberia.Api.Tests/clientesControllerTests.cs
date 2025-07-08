
using Barberia.API.Controllers;
using Barberia.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

public class ClientesControllerTests
{
    private ClientesController _controller;

    public ClientesControllerTests()
    {
        // Cada test comienza con un nuevo controlador para evitar efectos colaterales
        _controller = new ClientesController();

        // Limpiar lista estática antes de cada test
        var clientesField = typeof(ClientesController)
            .GetField("clientes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        clientesField.SetValue(null, new List<Cliente>());

        var nextIdField = typeof(ClientesController)
            .GetField("nextId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        nextIdField.SetValue(null, 1);
    }

    [Fact]
    public void Get_ReturnsEmptyListInitially()
    {
        var result = _controller.Get();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var clientes = Assert.IsType<List<Cliente>>(okResult.Value);
        Assert.Empty(clientes);
    }

    [Fact]
    public void Post_CreatesCliente_ReturnsCreated()
    {
        var nuevoCliente = new Cliente { Nombre = "Juan", Email = "juan@mail.com" };
        var result = _controller.Create(nuevoCliente);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var clienteCreado = Assert.IsType<Cliente>(createdResult.Value);
        Assert.Equal(1, clienteCreado.Id);
        Assert.Equal("Juan", clienteCreado.Nombre);
    }

    [Fact]
    public void GetById_ExistingId_ReturnsCliente()
    {
        var cliente = new Cliente { Nombre = "Ana" };
        _controller.Create(cliente);

        var result = _controller.GetById(1);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var clienteDevuelto = Assert.IsType<Cliente>(okResult.Value);
        Assert.Equal("Ana", clienteDevuelto.Nombre);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNotFound()
    {
        var result = _controller.GetById(999);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void Put_ExistingCliente_UpdatesAndReturnsNoContent()
    {
        var cliente = new Cliente { Nombre = "Luis" };
        _controller.Create(cliente);

        var clienteActualizado = new Cliente { Nombre = "Luis Actualizado", Email = "luis@mail.com" };
        var result = _controller.Update(1, clienteActualizado);

        Assert.IsType<NoContentResult>(result);

        var getResult = _controller.GetById(1);
        var okResult = Assert.IsType<OkObjectResult>(getResult.Result);
        var clienteDevuelto = Assert.IsType<Cliente>(okResult.Value);
        Assert.Equal("Luis Actualizado", clienteDevuelto.Nombre);
    }

    [Fact]
    public void Put_NonExistingCliente_ReturnsNotFound()
    {
        var clienteActualizado = new Cliente { Nombre = "No existe" };
        var result = _controller.Update(999, clienteActualizado);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ExistingCliente_ReturnsNoContent()
    {
        var cliente = new Cliente { Nombre = "Pedro" };
        _controller.Create(cliente);

        var result = _controller.Delete(1);
        Assert.IsType<NoContentResult>(result);

        var getResult = _controller.GetById(1);
        Assert.IsType<NotFoundResult>(getResult.Result);
    }

    [Fact]
    public void Delete_NonExistingCliente_ReturnsNotFound()
    {
        var result = _controller.Delete(999);
        Assert.IsType<NotFoundResult>(result);
    }
}
