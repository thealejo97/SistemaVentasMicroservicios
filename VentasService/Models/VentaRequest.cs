namespace VentasService.Models;

public class VentaRequest
{
    public int ClienteId { get; set; }
    public List<ItemVenta> Items { get; set; } = new();
}

public class ItemVenta
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}
