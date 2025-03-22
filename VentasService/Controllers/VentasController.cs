using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasService.Data;
using VentasService.Models;
using System.Text.Json;

namespace VentasService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HttpClient _http;

    public VentasController(AppDbContext context, IHttpClientFactory httpFactory)
    {
        _context = context;
        _http = httpFactory.CreateClient();
    }

    [HttpPost]
    public async Task<IActionResult> CrearVenta([FromBody] VentaRequest request)
    {
        // 1. Validar que el cliente exista
        var clienteResponse = await _http.GetAsync($"http://clientes-api:8080/api/clientes/{request.ClienteId}");
        if (!clienteResponse.IsSuccessStatusCode)
            return BadRequest("El cliente no existe");

        decimal total = 0;
        var detalles = new List<DetalleVenta>();

        // 2. Validar cada producto y calcular totales
        foreach (var item in request.Items)
        {
            var productoResponse = await _http.GetAsync($"http://productos-api:8080/api/productos/{item.ProductoId}");
            if (!productoResponse.IsSuccessStatusCode)
                return BadRequest($"Producto con ID {item.ProductoId} no encontrado");

            var json = await productoResponse.Content.ReadAsStringAsync();
            var producto = JsonSerializer.Deserialize<ProductoDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (producto == null || producto.Stock < item.Cantidad)
                return BadRequest($"Stock insuficiente para el producto {producto?.Nombre}");

            decimal subtotal = item.Cantidad * producto.Precio;
            total += subtotal;

            detalles.Add(new DetalleVenta
            {
                ProductoId = item.ProductoId,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio,
                Subtotal = subtotal
            });

            // 3. Descontar stock
            var descontar = new { cantidad = item.Cantidad };
            var content = new StringContent(JsonSerializer.Serialize(descontar), System.Text.Encoding.UTF8, "application/json");

            Console.WriteLine($"Descontando stock para producto {item.ProductoId} +++++ {content}");
            var response = await _http.PutAsync($"http://productos-api:8080/api/productos/{item.ProductoId}/descontar", content);
            Console.WriteLine($"Resultado: {response.StatusCode}");
        }

        // 4. Crear venta y guardar en la base de datos
        var venta = new Venta
        {
            ClienteId = request.ClienteId,
            Fecha = DateTime.UtcNow,
            Total = total,
            Detalles = detalles
        };

        _context.Ventas.Add(venta);
        await _context.SaveChangesAsync();

        return Ok(new { venta.Id, venta.Total});
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
    {
        var ventas = await _context.Ventas
            .Include(v => v.Detalles)
            .ToListAsync();

        return Ok(ventas);
    }

}
