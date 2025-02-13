using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nueva_practica.Data;
using Nueva_practica.Models;

namespace Nueva_practica.Controllers
{
    public class ClientesModelsController(AppDbContext context): ControllerBase
    {
        private readonly AppDbContext _context = context;



        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<ClientesModels>>>ObtenerClientes() 
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpPost("Agregar")]
        public async Task<IActionResult> AgregarCliente([FromBody] ClientesModels cliente)

        {
            if (cliente == null)
            {
                return BadRequest("Campos vacios");

            }
            var clienteExiste = await _context.Clientes.FirstOrDefaultAsync(x => x.Nombre == cliente.Nombre);
            if (clienteExiste != null) {
                return BadRequest("El cliente ya existe");
            
            }
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok("Cliente agregado con exito");
        }


        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClientesModels ActualizarCliente)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);

                if (cliente == null) {
                    return BadRequest("El cliente no existe");
                }
                cliente.Nombre = ActualizarCliente.Nombre;
                cliente.Dui = ActualizarCliente.Dui;
                cliente.Correo = ActualizarCliente.Correo;
                cliente.Direccion = ActualizarCliente.Direccion;
                cliente.Telefono = ActualizarCliente.Telefono;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok("Cliente actualizado");

            }
            catch (Exception ex) { 
               
                await transaction.RollbackAsync();  
                return Ok(ex.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return BadRequest("El cliente no existe");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok("El cliente fue eliminado");
        }

       
    }
}
