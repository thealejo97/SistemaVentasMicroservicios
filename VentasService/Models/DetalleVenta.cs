namespace VentasService.Models;
using System.Text.Json.Serialization;
public class DetalleVenta
{
    public int Id { get; set; }
    public int VentaId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
    [JsonIgnore]
    public Venta? Venta { get; set; }
}
