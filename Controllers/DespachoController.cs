using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TpfinalBack.Data;
using TpfinalBack.Filters;
using TpfinalBack.Models;

[SessionAuthorize]
[Route("Despacho/{action=Index}/{id?}")]
public class DespachoController : Controller
{
    private readonly ApplicationDbContext _context;

    public DespachoController(ApplicationDbContext context)
    {
        _context = context; 
    }

    // GET: Despacho/Index
    public async Task<IActionResult> Index()
    {
        var pendientes = await _context.Pedido
            .Include(p => p.Cliente)
            .Include(p => p.Usuario)
            .Where(p => !p.Confirmado)
            .OrderByDescending(p => p.fechaDespacho)
            .ToListAsync();

        return View(pendientes);
    }

    // POST: Despacho/Cancelar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancelar(int id)
    {
        var pedido = await _context.Pedido
            .Include(p => p.Detalles)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido != null && !pedido.Confirmado)
        {
            _context.DetallePedido.RemoveRange(pedido.Detalles);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Despacho/Nuevo
    public async Task<IActionResult> Nuevo()
    {
        ViewBag.Clientes = new SelectList(await _context.Cliente.OrderBy(c => c.Nombre).ToListAsync(), "Id", "Nombre");
        return View();
    }

    // POST: Despacho/Nuevo
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Nuevo(int clienteId)
    {
        var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
        if (!int.TryParse(usuarioIdStr, out int usuarioId)) return RedirectToAction("Login", "Account");

        var pedido = new Pedido
        {
            ClienteId = clienteId,
            UsuarioId = usuarioId,
            fechaDespacho = DateTime.Now,
            Confirmado = false,
            MontoTotal = 0
        };

        _context.Pedido.Add(pedido);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Editar), new { id = pedido.Id });
    }

    // GET: Despacho/Editar/5
    public async Task<IActionResult> Editar(int id)
    {
        var pedido = await _context.Pedido
            .Include(p => p.Cliente)
            .Include(p => p.Usuario)
            .Include(p => p.Detalles)
            .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null) return NotFound();

        ViewBag.Productos = await _context.Producto
            .OrderBy(p => p.Descripcion)
            .Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"[{p.Id}] {p.Descripcion} - ${p.CostoUnitario:N2}"
            })
            .ToListAsync();

        return View(pedido);
    }

    // POST: Despacho/AgregarProducto
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AgregarProducto(int pedidoId, int productoId, int cantidad)
    {
        var pedido = await _context.Pedido.Include(p => p.Detalles).FirstOrDefaultAsync(p => p.Id == pedidoId);
        if (pedido == null) return NotFound();  
        if (pedido.Confirmado) return RedirectToAction(nameof(Editar), new { id = pedidoId });

        var producto = await _context.Producto.FindAsync(productoId);
        if (producto == null)
        {
            TempData["Error"] = $"No existe un producto con ID {productoId}.";
            return RedirectToAction(nameof(Editar), new { id = pedidoId });
        }

        if (cantidad <= 0)
        {
            TempData["Error"] = "La cantidad debe ser mayor a 0.";
            return RedirectToAction(nameof(Editar), new { id = pedidoId });
        }

        var detalle = pedido.Detalles.FirstOrDefault(d => d.ProductoId == productoId);
        int cantidadYaEnPedido = detalle?.Cantidad ?? 0;
        if (cantidadYaEnPedido + cantidad > producto.stock)
        {
            TempData["Error"] = $"Stock insuficiente. Stock disponible: {producto.stock - cantidadYaEnPedido} unidades.";
            return RedirectToAction(nameof(Editar), new { id = pedidoId });
        }

        if (detalle != null)
        {
            detalle.Cantidad += cantidad;
            detalle.SubTotal = detalle.Cantidad * detalle.CostoUnitario;
            _context.Update(detalle);
        }
        else
        {
            detalle = new DetallePedido
            {
                PedidoId = pedidoId,
                ProductoId = productoId,
                Cantidad = cantidad,
                CostoUnitario = producto.CostoUnitario,
                SubTotal = cantidad * producto.CostoUnitario
            };
            _context.DetallePedido.Add(detalle);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Editar), new { id = pedidoId });
    }

    // POST: Despacho/EliminarDetalle/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarDetalle(int detalleId, int pedidoId)
    {
        var pedido = await _context.Pedido.FindAsync(pedidoId);
        if (pedido == null || pedido.Confirmado)
            return RedirectToAction(nameof(Editar), new { id = pedidoId });

        var detalle = await _context.DetallePedido.FindAsync(detalleId);
        if (detalle != null)
        {
            _context.DetallePedido.Remove(detalle);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Editar), new { id = pedidoId });
    }

    // POST: Despacho/Confirmar/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirmar(int id)
    {
        var pedido = await _context.Pedido
            .Include(p => p.Detalles)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null) return NotFound();
        if (pedido.Confirmado) return RedirectToAction(nameof(Editar), new { id });

        pedido.MontoTotal = pedido.Detalles.Sum(d => d.SubTotal);
        pedido.Confirmado = true;
        pedido.fechaDespacho = DateTime.Now;

        foreach (var detalle in pedido.Detalles)
        {
            var producto = await _context.Producto.FindAsync(detalle.ProductoId);
            if (producto != null)
            {
                producto.stock -= detalle.Cantidad;
                _context.Update(producto);
            }
        }

        _context.Update(pedido);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Editar), new { id });
    }
}
